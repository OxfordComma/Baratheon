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
	public Deck deck;



	public NetworkPlayer() { }

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
		DontDestroyOnLoad (this);
		this.name = "playerDude";
	}

	public override void OnStartLocalPlayer()
	{
		DontDestroyOnLoad (this);
		Debug.Log ("Starting local player");
	}

//    public void StartBattle()
//    {
//        Draw(3);
//    }

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

//    public void CmdShuffleDeck()
//    {
//        int n = deck.Count;
//        while (n > 1)
//        {
//            n--;
//            int k = Random.Range(1, n + 1);
//            string value = deck[k];
//            deck[k] = deck[n];
//            deck[n] = value;
//        }
//    }
//
//    public void Draw(int numCards)
//    {
//        for (int i = 0; i < numCards; i++)
//        {
//            GameObject cardInHandObject = GameObject.Instantiate(Resources.Load("Prefabs/Battlefield/CardInHand")) as GameObject;
//            cardInHandObject.GetComponent<CardInHand>().SetCard(GameController.GetGameController().set.GetCard(deck[0]));
//            cardInHandObject.name = deck[0];
//            cardInHandObject.transform.SetParent(GameObject.Find("Hand").transform, false);
//            cardsInHandSyncList.Add(deck[0]);
//            deck.Remove(deck[0]);
//        }
//    }

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
		var serializer = new XmlSerializer(typeof(NetworkPlayer));
		using (var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}

	public static NetworkPlayer Load(string path)
	{
		var serializer = new XmlSerializer(typeof(NetworkPlayer));
		using (var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as NetworkPlayer;
		}
	}
}
