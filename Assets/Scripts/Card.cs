using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;

public class Card {
	[XmlElement]
    public string Name;
    public string Type;
    public string Subtype;
    public string Element;
    public string Cost;
    public string Strength;
    public string Armor;
    public string Agility;
    public string Will;
    public string Traits;
    public string Text;
    public string Materials;

	public Card()
	{
		this.Name = "name";
	}

    public void Cast(Player player)
    {
        Debug.Log("Casting");

        CardFunctionsHandler cfh = new CardFunctionsHandler(player);
        cfh.AncestralRecall();
    }
}
