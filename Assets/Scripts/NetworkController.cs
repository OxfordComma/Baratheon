using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Collections;

public class NetworkController : MonoBehaviour {

    public NetworkPlayer networkPlayer;
	public NetworkClient client;
	public GameObject networkPlayerPrefab;
	//public static GameObject networkController;
	public string playerName;
	void Start()
	{
		//on client, this isn't required but is nice for testing.
		Application.runInBackground = true;
		DontDestroyOnLoad (this);

		client = new NetworkClient();
		client.RegisterHandler(MsgType.Connect, OnClientConnected);
		ClientScene.RegisterPrefab (networkPlayerPrefab);
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
