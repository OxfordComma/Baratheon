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
            NetworkController.GetNetworkController().client.Connect("192.168.0.103", 7777);
            SceneManager.LoadScene ("Main Menu");
		}
	}
}