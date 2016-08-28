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

	public static DeckXML Load(string path)
	{
		var serializer = new XmlSerializer(typeof(Deck));
		using (var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as DeckXML;
		}
	}
}

