using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Xml.Serialization;
using UnityEngine  .SceneManagement;

[XmlRoot("player")]
public class NetworkPlayer : NetworkBehaviour {
	[SyncVar]
	public string playerName;
	Deck deck;
	public SyncListString syncDeck;

	public NetworkPlayer() { }

    [ClientRpc]
    public void RpcGoToMainMenu()
    {
        NetworkController.GetNetworkController().networkPlayer = this;
        SceneManager.LoadScene("Main Menu");
    }

    //    [Command]
    //    public void CmdSetName(string name)
    //    {
    //        this.name = name;
    //        this.playerName = name;
    //    }

    public void Start()
	{
		DontDestroyOnLoad (this);
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
		PlayerXML pxml = new PlayerXML (this);
		pxml.Save (Path.Combine (Application.persistentDataPath, "users/" + this.name + "/player.xml"));
	}

	public void LoadFromXML(string playerName)
	{
		PlayerXML pxml = PlayerXML.Load (Path.Combine (Application.persistentDataPath, "users/" + playerName + "/player.xml"));
		this.name = pxml.name;
		this.deck = pxml.XMLDeck.ToDeck ();
		this.syncDeck = deck.ToSyncListString ();
	}

//	public void Save(string path)
//	{
//		var serializer = new XmlSerializer(typeof(NetworkPlayer));
//		using (var stream = new FileStream(path, FileMode.Create))
//		{
//			serializer.Serialize(stream, this);
//		}
//	}
//
//	public static NetworkPlayer Load(string path)
//	{
//		var serializer = new XmlSerializer(typeof(NetworkPlayer));
//		using (var stream = new FileStream(path, FileMode.Open))
//		{
//			return serializer.Deserialize(stream) as NetworkPlayer;
//		}
//	}
}
