using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

[XmlRoot("player")]
public class NetworkPlayer : NetworkBehaviour {

	[SyncVar]
	public string playerName;

	public SyncListString deckSyncList;
    public SyncListString cardsInHandSyncList;

	public Player player;

	public NetworkPlayer()
	{
		deckSyncList = new SyncListString();
		cardsInHandSyncList = new SyncListString();
	}

	public NetworkPlayer(string username, Deck deck)
	{
		this.name = username;
		deckSyncList = deck.ToSyncListString ();
		cardsInHandSyncList = new SyncListString();
	}

	public NetworkPlayer (Player player)
	{

	}

	[ClientRpc]
	public void RpcSetName()
	{
        CmdSetName(NetworkController.GetNetworkController().playerName);
	}

    [Command]
    public void CmdSetName(string name)
    {
        this.name = name;
        this.playerName = name;
    }


    public void Start()
	{
		this.name = "playerDude";
		DontDestroyOnLoad (this);
	}

	public override void OnStartLocalPlayer()
	{
		DontDestroyOnLoad (this);
		Debug.Log ("Starting local player");
	}

	public Deck deck
	{
		get { return player.deck; }
		set { player.deck = value; }
	}

    public void StartBattle()
    {
        Draw(3);
    }

	[Command]
	public void CmdAddCardToDeck(Card card)
	{
		deck.AddCard(card);
	}

	[Command]
	public void CmdRemoveCardFromDeck(Card card)
	{
		deck.RemoveCard (card);
	}

    public void CmdShuffleDeck()
    {
        int n = deckSyncList.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(1, n + 1);
            string value = deckSyncList[k];
            deckSyncList[k] = deckSyncList[n];
            deckSyncList[n] = value;
        }
    }

    public void Draw(int numCards)
    {
        for (int i = 0; i < numCards; i++)
        {
            GameObject cardInHandObject = GameObject.Instantiate(Resources.Load("Prefabs/Battlefield/CardInHand")) as GameObject;
            cardInHandObject.GetComponent<CardInHand>().SetCard(GameController.GetGameController().set.GetCard(deckSyncList[0]));
            cardInHandObject.name = deckSyncList[0];
            cardInHandObject.transform.SetParent(GameObject.Find("Hand").transform, false);
            cardsInHandSyncList.Add(deckSyncList[0]);
            deckSyncList.Remove(deckSyncList[0]);
        }
    }

	public bool CanAddCardToDeck(Card card)
	{
		return true;
	}

	public void SaveToXML()
	{
		Save (Path.Combine (Application.persistentDataPath, "users/" + this.name + "/player.xml"));
	}

	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(Player));
		using (var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}

	public static Player Load(string path)
	{
		var serializer = new XmlSerializer(typeof(Player));
		using (var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as Player;
		}
	}
}
