using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;

//public enum element {fire, water, earth, air};

public class Card {
	public string name;
    public string type;
    public string subtype;
    public string element;
    public string cost;
    public string strength;
    public string armor;
    public string agility;
    public string will;
    public string traits;
    public string text;
    public string materials;

	public Card()
	{
		this.name = "defaultName";
	}

	public Card(CardXML cardxml)
	{
		this.name = cardxml.Name;
	}

    public void Cast(Player player)
    {
        Debug.Log("Casting");

        CardFunctionsFactory cfh = new CardFunctionsFactory(player);
        cfh.AncestralRecall();
    }
}
