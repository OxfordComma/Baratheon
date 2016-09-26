using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System;
using System.Linq;

public class BaratheonNetworkServer : MonoBehaviour
{
	public GameObject playerPrefab;
    public List<GameObject> connectedPlayers;
	void Awake()
	{
		DontDestroyOnLoad (this);
		Application.runInBackground = true;
		NetworkServer.RegisterHandler(MsgType.Connect, OnConnect);
		NetworkServer.RegisterHandler(MsgType.AddPlayer, OnAddPlayer);
        NetworkServer.RegisterHandler(MsgType.RemovePlayer, OnRemovePlayer);
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
        connectedPlayers.Add(player);
	}

    private void OnRemovePlayer(NetworkMessage netMsg)
    {
        GameObject player = netMsg.conn.playerControllers[0].gameObject;
        NetworkServer.UnSpawn(player);
        Destroy(player);
        connectedPlayers.Find(playerObj => playerObj = player);
    }

	private void OnPlayerDisconnect(NetworkMessage netMsg)
	{
		GameObject player = netMsg.conn.playerControllers[0].gameObject;
		NetworkServer.UnSpawn(player);
		Destroy(player);
        connectedPlayers.Find(playerObj => playerObj = player);
	}
}