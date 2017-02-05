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
		DontDestroyOnLoad (this);
		Application.runInBackground = true;


		WriteToConsole(NetworkServer.Listen(7777).ToString());
		/*
		NetworkServer.RegisterHandler(MsgType.Connect, OnConnect);
		NetworkServer.RegisterHandler(MsgType.AddPlayer, OnAddPlayer);
		NetworkServer.RegisterHandler(MsgType.RemovePlayer, OnRemovePlayer);
		NetworkServer.RegisterHandler(MsgType.Disconnect, OnDisconnect);
		ClientScene.RegisterPrefab (playerPrefab);


		WriteToConsole ("Server started.");
		*/
	}

	void WriteToConsole(string text)
	{
		/*
		if (consoleWindowText.text == "") {
			consoleWindowText.text = text;
			return;
		}*/
		consoleWindowText.text += text + "\n";
	}

	private void OnConnect(NetworkMessage netMsg)
	{
		WriteToConsole("Player connected!");
	}

	private void OnAddPlayer(NetworkMessage netMsg)
	{
		
		GameObject player = Instantiate<GameObject>(playerPrefab);
		NetworkServer.AddPlayerForConnection(netMsg.conn, player, 0);
		//player.GetComponent<NetworkPlayer> ().RpcSetName ();

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