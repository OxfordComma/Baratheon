using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Serialization;

public class LoginField : MonoBehaviour {
	public InputField infield;
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			NetworkController.GetNetworkController ().playerName = infield.text;
			Connect ();
			SceneManager.LoadScene ("Main Menu");
//			NetworkController.GetNetworkController ().client.RegisterHandler (MsgType.Connect, OnConnect);
//			NetworkController.GetNetworkController ().client.Connect ("127.0.0.1", 7777);
		}
	}

//	public void OnConnect (NetworkMessage netMsg)
//	{
//		ClientScene.RegisterPrefab (Resources.Load ("Prefabs/NetworkPlayer") as GameObject);
//		StringMessage nameStringMessage = new StringMessage (GameObject.Find ("LoginName").GetComponent<Text> ().text);
//		ClientScene.AddPlayer (netMsg.conn, 0, nameStringMessage);
//		Debug.Log ("Connected. Adding player");
//	}

	public void Connect()
	{
		NetworkController.client.Connect ("127.0.0.1", 7777);
	}
}