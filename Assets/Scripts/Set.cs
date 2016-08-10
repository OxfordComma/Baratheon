using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class Set
{
	public List<Card> cards;
	public Set()
	{
		this.cards = new List<Card> ();
	}

	public Set(SetXML setxml)
	{
		this.cards = new List<Card> ();
		for (int i = 0; i < setxml.cards.Count; i++) {
			Card card = new Card (setxml.cards [i]);
			cards.Add (card);
		}
	}

	/*
	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(Set));
		using (var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}

	public static Set Load(string path)
	{
		var serializer = new XmlSerializer(typeof(Set));
		using (var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as Set;
		}
	}
	*/
}

