using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class HostGameButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void HostGame()
	{
		NetworkServer.Listen (7777);

	}
}
