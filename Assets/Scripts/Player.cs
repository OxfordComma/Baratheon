using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("player")]
public class Player
{
	public string name;
	[XmlElement]
	public Deck deck;

	public Player()
	{
		this.name = "playerDude";
		this.deck = new Deck();
	}
		
//    public Player(string name)
//    {
//        this.name = name;
//		this.deck = new Deck();
//    }gr

	public Player(string name, Deck deck)
	{
		this.name = name;
		this.deck = deck;
	}

	public void SaveToXML(NetworkPlayer networkPlayer)
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

