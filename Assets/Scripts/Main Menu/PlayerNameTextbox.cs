using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerNameTextbox : MonoBehaviour {
	public Text playerNameText;
	// Use this for initialization
	void Start () {
		
	}

	void Awake () {
		PlayerHandler playerHandler = GameObject.Find ("Player Handler").GetComponent<PlayerHandler> ();
		playerNameText.text = playerHandler.currentPlayer.name;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
