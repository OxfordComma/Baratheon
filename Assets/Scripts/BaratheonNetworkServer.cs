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
		short PlayerFinishedLoading = 1002;

		DontDestroyOnLoad (this);
		Application.runInBackground = true;

		NetworkServer.Listen(7777);


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

	private void OnAddPlayer(NetworkMessage netMsg)
	{
		netMsg.conn.playerControllers [0].gameObject.GetComponent<NetworkPlayer> ().RpcGoToMainMenu ();
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
			WriteToConsole ("New player.");
			Directory.CreateDirectory (Path.Combine (Application.persistentDataPath, "users/" + networkPlayer.playerName));
			
			networkPlayer.deck = new Deck ();
			networkPlayer.SaveToXML ();
		}
		else {
			networkPlayer.LoadFromXML (playerName);
			WriteToConsole ("Loading player " + playerName);
		}

		player.gameObject.name = playerName;
		networkPlayer.playerName = playerName;

		NetworkServer.AddPlayerForConnection(netMsg.conn, player, 0);
		
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