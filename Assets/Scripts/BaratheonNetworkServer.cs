using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;

public class BaratheonNetworkServer : MonoBehaviour
{
	public Text consoleWindowText;
	public GameObject playerPrefab;
    public List<GameObject> connectedPlayers;

	void Start()
	{
		const short PlayerNameMessage = 1001;

		DontDestroyOnLoad (this);
		Application.runInBackground = true;


		NetworkServer.Listen(7777);

		NetworkServer.RegisterHandler(MsgType.Connect, OnConnect);
		NetworkServer.RegisterHandler(MsgType.AddPlayer, OnAddPlayer);
		NetworkServer.RegisterHandler(MsgType.RemovePlayer, OnRemovePlayer);
		NetworkServer.RegisterHandler(MsgType.Disconnect, OnDisconnect);
//		Player name
		NetworkServer.RegisterHandler(PlayerNameMessage, OnSendPlayerName);
		ClientScene.RegisterPrefab (playerPrefab);


		WriteToConsole ("Server started.");

	}

	void WriteToConsole(string text)
	{
		consoleWindowText.text += text + "\n";
	}

	private void OnConnect(NetworkMessage netMsg)
	{
		WriteToConsole("Player connected!");
	}

	private void OnServerAddPlayer(NetworkMessage netMsg, short playerControllerId)
	{
		WriteToConsole ("OnServerAddPlayer");
	}

//	Add player once character name is sent
	private void OnSendPlayerName(NetworkMessage netMsg)
	{
		var playerName = netMsg.ReadMessage<StringMessage> ().value;
		WriteToConsole (playerName);

	}

	private void OnAddPlayer(NetworkMessage netMsg)
	{
		GameObject player = Instantiate<GameObject>(playerPrefab);
		NetworkServer.AddPlayerForConnection(netMsg.conn, player, 0);
		player.GetComponent<NetworkPlayer> ().RpcSetName ();

		WriteToConsole (player.GetComponent<NetworkPlayer>().playerName);

	}

    private void OnRemovePlayer(NetworkMessage netMsg)
    {
        GameObject player = netMsg.conn.playerControllers[0].gameObject;
        NetworkServer.UnSpawn(player);
        Destroy(player);
    }

	private void OnDisconnect(NetworkMessage netMsg)
	{
		WriteToConsole ("Player disconnected.");
		GameObject player = netMsg.conn.playerControllers[0].gameObject;
		NetworkServer.UnSpawn(player);
		Destroy(player);
	}

	public NetworkPlayer LoadPlayer(string playerName)
	{
		NetworkPlayer networkPlayer = new NetworkPlayer();
		if (!Directory.Exists (Path.Combine (Application.persistentDataPath, "users")))
			Directory.CreateDirectory (Path.Combine (Application.persistentDataPath, "users"));

		string userPath = Path.Combine (Application.persistentDataPath, "users").ToString ();
		string[] users = Directory.GetDirectories (userPath).Select (path => Path.GetFileName (path)).ToArray ();

		if (!users.Contains (playerName)) {
			Directory.CreateDirectory (Path.Combine (Application.persistentDataPath, "users/" + playerName));
			networkPlayer.playerName = playerName;
			networkPlayer.deck = new Deck ();
			networkPlayer.SaveToXML ();
		}
		else {
			networkPlayer = NetworkPlayer.Load (Path.Combine (Application.persistentDataPath, "users/" + networkPlayer.playerName + "/player.xml"));
		}

		return networkPlayer;
	}
}