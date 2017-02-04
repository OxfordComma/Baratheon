using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkController : MonoBehaviour {

	public static NetworkClient client;
	public GameObject networkPlayerPrefab;
	public static GameObject networkController;
	public string playerName;
	void Start()
	{
		//on client, this isn't required but is nice for testing.
		Application.runInBackground = true;
		DontDestroyOnLoad (this);

		client = new NetworkClient();
		//client.Connect("localhost", 7777);
		client.RegisterHandler(MsgType.Connect, OnClientConnected);

		ClientScene.RegisterPrefab (networkPlayerPrefab);
		networkController = this.gameObject;
	}

	private void OnClientConnected(NetworkMessage netMsg)
	{
		ClientScene.Ready(netMsg.conn);
		ClientScene.AddPlayer(0);
	}

	public static NetworkController GetNetworkController()
	{
		return GameObject.Find ("NetworkController").GetComponent<NetworkController> ();
	}


}
