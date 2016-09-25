using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System;

public class BaratheonNetworkServer : MonoBehaviour
{
	public GameObject playerPrefab;
	void Awake()
	{
		DontDestroyOnLoad (this);
		Application.runInBackground = true;
		NetworkServer.RegisterHandler(MsgType.Connect, OnConnect);
		NetworkServer.RegisterHandler(MsgType.AddPlayer, OnAddPlayer);
		NetworkServer.RegisterHandler(MsgType.Disconnect, OnPlayerDisconnect);
		NetworkServer.Listen(7777);

	}

	private void OnConnect(NetworkMessage netMsg)
	{
		Debug.Log("Player connected!");
	}

	private void OnAddPlayer(NetworkMessage netMsg)
	{
		GameObject player = Instantiate<GameObject>(playerPrefab);
		NetworkServer.AddPlayerForConnection(netMsg.conn, player, 0);
	}

	private void OnPlayerDisconnect(NetworkMessage netMsg)
	{
		GameObject player = netMsg.conn.playerControllers[0].gameObject;
		NetworkServer.UnSpawn(player);
		Destroy(player);
	}
}