using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class ConnectedPlayerList : MonoBehaviour {
    Text text;
	// Use this for initialization
	void Start () {
        text = this.GetComponent<Text>();

        foreach (NetworkConnection n in NetworkServer.connections)
        {
            if (n == null)
                continue;
            Debug.Log(n.playerControllers[0].gameObject.name);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
        
    }
}
