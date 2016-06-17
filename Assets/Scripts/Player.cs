using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class Player
{
	public string name;
	public Deck deck;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public Player()
	{
		this.name = "playerName";
		this.deck = new Deck();
	}

	public Player(PlayerXML playerxml)
	{
		this.name = playerxml.name;
	}

    public Player(string name)
    {
        this.name = name;
		this.deck = new Deck();
    }

	public void SaveToXML()
	{
		PlayerXML playerxml = new PlayerXML (this);
		playerxml.Save (Path.Combine (Application.persistentDataPath, "users/" + this.name + "/player.xml"));
	}
	/*
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
	
    public void AddCardToDeck(Card card)
    {
        deck.AddCard(card);
    }

	public void RemoveCardFromDeck(Card card)
	{
		deck.RemoveCard (card);
	}

	public bool CanAddCardToDeck(Card card)
	{
		return true;
	}
	*/
}

