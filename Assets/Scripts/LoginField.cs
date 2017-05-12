using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Serialization;

public class LoginField : NetworkBehaviour {
	public InputField infield;
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
            NetworkController nc = NetworkController.GetNetworkController();
            nc.playerName = infield.text;
            nc.client.Connect("192.168.0.105", 7777);
		}
	}
}