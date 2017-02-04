using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.IO;
using System.Xml.Serialization;

public class LoginHandler : MonoBehaviour {
    PlayerHandler playerHandler;

	// Use this for initialization
	void Start () {
	}

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			Login();
		}
	}

	public void Login()
	{
		SetXML setxml = new SetXML ();
		setxml.cards.Add(new CardXML());
		setxml.Save (Path.Combine (Application.persistentDataPath, "testplayer.xml"));
		//Debug.Log (Application.persistentDataPath);

		if (!Directory.Exists (Path.Combine (Application.persistentDataPath, "users")))
			Directory.CreateDirectory(Path.Combine (Application.persistentDataPath, "users"));
		
		string userPath = Path.Combine (Application.persistentDataPath, "users").ToString();
        string[] users = Directory.GetDirectories(userPath).Select(path => Path.GetFileName(path)).ToArray();

        string nameInLoginField = GameObject.Find("LoginName").GetComponent<Text>().text;

		if (!users.Contains (nameInLoginField))
			CreateLogin (nameInLoginField);
		else 
		{
			playerHandler.activePlayer = 
				Player.Load (Path.Combine (Application.persistentDataPath, "users/" + nameInLoginField + "/player.xml")
			);
		}

		Navigation.StaticGoToMainMenu ();
	}

    public void CreateLogin(string username)
    {
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "users/" + username));
        Player newPlayer = new Player(username, new Deck());
        //newPlayer.CmdSaveToXML()();
        playerHandler.activePlayer = newPlayer;
    }

    public void Logout()
	{
		GameController.GetGameController().localPlayer.SaveToXML();
		GameController.GetGameController().localPlayer = null;
		GameController.GoToLoginScreen();
	}
}
