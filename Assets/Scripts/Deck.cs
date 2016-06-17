using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;

//[XmlRoot("deck")]
public class Deck
{
	//[XmlElement("card")]
	public List<Card> cards;
    // Use this for initialization
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {

    }

	public Deck()
	{
		this.cards = new List<Card>();
	}

	public Deck(DeckXML deckxml)
	{
		foreach (CardXML cardxml in deckxml.cards) {
			cards.Add (new Card (cardxml));
		}
	}
	/*
	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(Deck));
		using (var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}

	public static Deck Load(string path)
	{
		var serializer = new XmlSerializer(typeof(Deck));
		using (var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as Deck;
		}
	}
	*/
    public void AddCard(Card card)
    {
		cards.Add(card);
    }

	public void RemoveCard(Card card)
	{
		cards.Remove (card);
	}	

    public void Shuffle()
    {
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(1, n + 1);
            Card value = cards[k];
            cards[k] = cards[n];
            cards[n] = value;
        }
    }
}

