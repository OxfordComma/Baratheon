using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;

using System.Collections;

public class NetworkController : NetworkBehaviour {

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
        client.RegisterHandler(MsgType.AddPlayer, OnClientAddPlayer);
		client.RegisterHandler(MsgType.Connect, OnClientConnected);
        client.RegisterHandler(MsgType.Ready, OnClientReady);
		ClientScene.RegisterPrefab (networkPlayerPrefab);
	}

    private void OnClientAddPlayer(NetworkMessage netMsg)
    {
        networkPlayer = GameObject.FindWithTag("Player").GetComponent<NetworkPlayer>();
        SceneManager.LoadScene("Main Menu");

    }

    private void OnClientConnected(NetworkMessage netMsg)
	{
        Debug.Log(playerName);
        client.Send(1001, new StringMessage(playerName));
        ClientScene.Ready(netMsg.conn);
        ClientScene.AddPlayer(0);
    }

    private void OnClientReady(NetworkMessage netMsg)
    {
        Debug.Log("Ready");
    }

    public static NetworkController GetNetworkController()
	{
		return GameObject.Find ("NetworkController").GetComponent<NetworkController> ();
	}

    public static NetworkPlayer GetNetworkPlayer()
    {
        return GetNetworkController().networkPlayer;
    }


}
