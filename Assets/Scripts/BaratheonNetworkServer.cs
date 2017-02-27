using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;

public class BaratheonNetworkServer : NetworkBehaviour
{
	public Text consoleWindowText;
	public GameObject playerPrefab;
    public List<GameObject> connectedPlayers;

	void Start()
	{
		short PlayerNameMessage = 1001;

		DontDestroyOnLoad (this);
		Application.runInBackground = true;

		NetworkServer.Listen(7777);


		NetworkServer.RegisterHandler(MsgType.Connect, OnConnect);
		NetworkServer.RegisterHandler(MsgType.AddPlayer, OnAddPlayer);
		NetworkServer.RegisterHandler(MsgType.RemovePlayer, OnRemovePlayer);
		NetworkServer.RegisterHandler(MsgType.Disconnect, OnDisconnect);
//		Player name
		NetworkServer.RegisterHandler(1001, OnSendPlayerName);
		ClientScene.RegisterPrefab (playerPrefab);


		WriteToConsole ("Server started.");

	}

	void WriteToConsole(string text)
	{
		consoleWindowText.text += text + "\n";
	}

	private void OnConnect(NetworkMessage netMsg)
	{
	}

//	Add player once character name is sent
	private void OnSendPlayerName(NetworkMessage netMsg)
	{
		WriteToConsole("Player connected!");

		var playerNameMsg = netMsg.ReadMessage<StringMessage> ();
		string playerName = playerNameMsg.value;
		WriteToConsole ("Player name received: " + playerName);

//		Load from disk
		if (!Directory.Exists (Path.Combine (Application.persistentDataPath, "users")))
			Directory.CreateDirectory (Path.Combine (Application.persistentDataPath, "users"));

		string userPath = Path.Combine (Application.persistentDataPath, "users").ToString ();
		string[] users = Directory.GetDirectories (userPath).Select (path => Path.GetFileName (path)).ToArray ();

		GameObject player = Instantiate<GameObject>(playerPrefab);
		NetworkPlayer networkPlayer = player.GetComponent<NetworkPlayer>();

		if (!users.Contains (playerName)) {
			Directory.CreateDirectory (Path.Combine (Application.persistentDataPath, "users/" + networkPlayer.playerName));
			
			networkPlayer.deck = new Deck ();
			networkPlayer.SaveToXML ();
		}
		else {
			networkPlayer = NetworkPlayer.LoadFromXML (Path.Combine (Application.persistentDataPath, "users/" + playerName + "/player.xml"));
		}

		player.gameObject.name = playerName;
		networkPlayer.playerName = playerName;

		NetworkServer.AddPlayerForConnection(netMsg.conn, player, 0);
		
	}

	private void OnAddPlayer(NetworkMessage netMsg)
	{
		
	}

    private void OnRemovePlayer(NetworkMessage netMsg)
    {
		WriteToConsole ("Player removed.");
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

}