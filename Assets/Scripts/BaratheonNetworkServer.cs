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
//		var playerName = netMsg.ReadMessage<StringMessage> ().value;
//		WriteToConsole (playerName);
		GameObject player = Instantiate<GameObject>(playerPrefab);
//		player.name = playerName;
		NetworkServer.AddPlayerForConnection(netMsg.conn, player, 0);
		player.GetComponent<NetworkPlayer> ().RpcSetName ();

		WriteToConsole (player.GetComponent<NetworkPlayer>().playerName);
		//NetworkServer.Spawn (playerPrefab);
		//connectedPlayers.Add(player);

	}

    private void OnRemovePlayer(NetworkMessage netMsg)
    {
        GameObject player = netMsg.conn.playerControllers[0].gameObject;
        NetworkServer.UnSpawn(player);
        Destroy(player);
        //connectedPlayers.Find(playerObj => playerObj = player);
    }

	private void OnDisconnect(NetworkMessage netMsg)
	{
		WriteToConsole ("Player disconnected.");
		GameObject player = netMsg.conn.playerControllers[0].gameObject;
		NetworkServer.UnSpawn(player);
		Destroy(player);
        //connectedPlayers.Find(playerObj => playerObj = player);
	}

	public NetworkPlayer LoadPlayer(string username)
	{
		NetworkPlayer networkPlayer;
		if (!Directory.Exists (Path.Combine (Application.persistentDataPath, "users")))
			Directory.CreateDirectory (Path.Combine (Application.persistentDataPath, "users"));

		string userPath = Path.Combine (Application.persistentDataPath, "users").ToString ();
		string[] users = Directory.GetDirectories (userPath).Select (path => Path.GetFileName (path)).ToArray ();

		if (!users.Contains (username)) {
			Directory.CreateDirectory (Path.Combine (Application.persistentDataPath, "users/" + username));
			networkPlayer = new NetworkPlayer (username, new Deck());
			networkPlayer.SaveToXML ();
		}
		else {
			Player player = Player.Load (Path.Combine (Application.persistentDataPath, "users/" + username + "/player.xml"));
			networkPlayer = new NetworkPlayer (player);
		}

		return networkPlayer;

//		this.playerName = player.name;
//		foreach (Card card in player.deck.cards)
//			deckSyncList.Add (card.name);
//
//		this.transform.name = player.name + " (Network)";
	}
}