using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;

[XmlRoot("deck")]
public class DeckXML : CardGroupXML
{
	[XmlElement("card")]
	public List<CardXML> cards;
    
	public DeckXML()
	{
		this.cards = new List<CardXML>();
	}

	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(DeckXML));
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
	/*
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
    */
}

