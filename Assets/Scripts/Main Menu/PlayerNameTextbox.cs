using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerNameTextbox : MonoBehaviour {
	public Text playerNameText;
	// Use this for initialization
	void Start () {
	}

	void Awake () {
//		playerNameText.text = GameController.GetGameController().localPlayer.playerName;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerNameText.text == "PlayerNameNotSet")
			playerNameText.text = GameObject.FindWithTag("Player").GetComponent<NetworkPlayer>().playerName;
		
	}
}
