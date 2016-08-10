using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.IO;
using System.Xml.Serialization;

public class LoginField : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			Login();
		}
	}

	public void Login()
	{
		/*
		SetXML setxml = new SetXML ();
		setxml.cards.Add(new CardXML());
		setxml.Save (Path.Combine (Application.persistentDataPath, "testplayer.xml"));
		//Debug.Log (Application.persistentDataPath);
		*/

		if (!Directory.Exists (Path.Combine (Application.persistentDataPath, "users")))
			Directory.CreateDirectory(Path.Combine (Application.persistentDataPath, "users"));

		string userPath = Path.Combine (Application.persistentDataPath, "users").ToString();
		string[] users = Directory.GetDirectories(userPath).Select(path => Path.GetFileName(path)).ToArray();

		string nameInLoginField = GameObject.Find("LoginName").GetComponent<Text>().text;

		if (!users.Contains (nameInLoginField))
			CreatePlayer (nameInLoginField);
		else 
		{
			LoadExistingPlayer (nameInLoginField);
		}

		Navigation.StaticGoToMainMenu ();
	}

	public void CreatePlayer(string username)
	{
		Directory.CreateDirectory (Path.Combine (Application.persistentDataPath, "users/" + username));
		Player newPlayer = new Player(username);
		newPlayer.SaveToXML ();
		GameController.GetGameController().localPlayer = newPlayer;
	}

	public void LoadExistingPlayer(string username)
	{
		GameController.GetGameController().localPlayer =
			Player.Load (Path.Combine (Application.persistentDataPath, "users/" + username + "/player.xml")
		);
	}
}
