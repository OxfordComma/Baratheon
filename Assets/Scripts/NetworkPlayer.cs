using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

public class NetworkPlayer : NetworkBehaviour {

	[SyncVar]
	public string playerName;

	public SyncListString deckSyncList;

	public Player player;
//	public Deck deck;

	public void Awake()
	{
		deckSyncList = new SyncListString ();

	}

	public void Start()
	{
		DontDestroyOnLoad (this);
	}

	public override void OnStartLocalPlayer()
	{
		DontDestroyOnLoad (this);
		Debug.Log ("Starting local player");
 		string username = GameObject.Find("LoginName").GetComponent<Text>().text;

		CmdLoadPlayer (username);

		Navigation.StaticGoToMainMenu ();

	}

	public Deck deck
	{
		get { return player.deck; }
		set { player.deck = value; }
	}

	[Command]
	public void CmdLoadPlayer(string username)
	{
		if (!Directory.Exists (Path.Combine (Application.persistentDataPath, "users")))
			Directory.CreateDirectory (Path.Combine (Application.persistentDataPath, "users"));

		string userPath = Path.Combine (Application.persistentDataPath, "users").ToString ();
		string[] users = Directory.GetDirectories (userPath).Select (path => Path.GetFileName (path)).ToArray ();

		if (!users.Contains (username)) {
			Directory.CreateDirectory (Path.Combine (Application.persistentDataPath, "users/" + username));
			this.player = new Player (username, new Deck());
			player.SaveToXML ();
		}
		else {
			this.player = Player.Load (Path.Combine (Application.persistentDataPath, "users/" + username + "/player.xml"));
		}

		this.playerName = player.name;
		foreach (Card card in player.deck.cards)
			deckSyncList.Add (card.name);
		
		this.transform.name = player.name + " (Network)";
	}

	[Command]
	public void CmdAddCardToDeck(Card card)
	{
		deck.AddCard(card);
		
		deckSyncList.Add (card.name);
	}

	[Command]
	public void CmdRemoveCardFromDeck(Card card)
	{
		deck.RemoveCard (card);
		
		deckSyncList.Remove (card.name);
	}

	public bool CanAddCardToDeck(Card card)
	{
		return true;
	}

	public void SaveToXML()
	{
		player.SaveToXML ();
	}
}
