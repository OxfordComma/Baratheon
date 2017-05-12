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
	public List<CardXML> XMLCards;
    
	public DeckXML()
	{
		this.XMLCards = new List<CardXML>();
	}

	public Deck ToDeck()
	{
		Deck deck = new Deck ();
		foreach (CardXML cxml in XMLCards) {
			deck.AddCard (cxml.ToCard ());
		}
		return deck;
	}

	public Deck ToSyncDeck()
	{
		Deck deck = new Deck ();
		foreach (CardXML cxml in XMLCards) {
			deck.AddCard (cxml.ToCard ());
		}
		return deck;
	}

	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(DeckXML));
		using (var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}

	public static DeckXML Load(string path)
	{
		var serializer = new XmlSerializer(typeof(Deck));
		using (var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as DeckXML;
		}
	}
}

