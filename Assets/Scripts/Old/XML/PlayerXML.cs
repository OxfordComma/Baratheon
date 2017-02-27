﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("player")]
public class PlayerXML
{
	public string name;
	public DeckXML XMLDeck;

	public PlayerXML()
	{
		this.name = "defaultName";
		this.XMLDeck = new DeckXML ();
	}

	public PlayerXML(NetworkPlayer player)
	{
		this.name = player.name;
		this.XMLDeck = new DeckXML ();
		foreach (Card card in player.deck.cards)
			this.XMLDeck.XMLCards.Add (new CardXML (card.name));
		
	}

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(PlayerXML));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static PlayerXML Load(string path)
    {
        var serializer = new XmlSerializer(typeof(PlayerXML));
        using (var stream = new FileStream(path, FileMode.Open))
        {
			PlayerXML pxml = serializer.Deserialize(stream) as PlayerXML;
			return pxml;
        }
    }
}

