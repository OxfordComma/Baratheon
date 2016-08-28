using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkController : MonoBehaviour {

	public NetworkClient client;
	//private NetworkIdentity _networkStateEntityProtoType;
	void Start()
	{
		//on client, this isn't required but is nice for testing.
		Application.runInBackground = true;

		//var globals = FindObjectOfType<GlobalAssets>();

		//_networkStateEntityProtoType = globals.NetworkEntityStatePrototype.GetComponent<NetworkIdentity>();

		//ClientScene.RegisterSpawnHandler(_networkStateEntityProtoType.assetId, OnSpawnEntity, OnDespawnEntity);

		client = new NetworkClient();
		//client.Connect("localhost", 7777);
		client.RegisterHandler(MsgType.Connect, OnClientConnected);

	}

	private void OnDespawnEntity(GameObject spawned)
	{
		Destroy(spawned);
	}

	private void OnClientConnected(NetworkMessage netMsg)
	{
		ClientScene.Ready(netMsg.conn);
		ClientScene.AddPlayer(0);
	}
	/*
	private GameObject OnSpawnEntity(Vector3 position, NetworkHash128 assetId)
	{
		var networkEntity = Instantiate<NetworkIdentity>(_networkStateEntityProtoType);

		networkEntity.transform.SetParent(this.transform);
		return networkEntity.gameObject;
	}
	*/
	public static NetworkController GetNetworkController()
	{
		return GameObject.FindWithTag ("NetworkController").GetComponent<NetworkController> ();
	}
}
