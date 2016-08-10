using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;

public class Card {
	[XmlElement]
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
		this.type = cardxml.Type;
		this.subtype = cardxml.Subtype;
		this.element = cardxml.Element;
		this.cost = cardxml.Cost;
		this.strength = cardxml.Strength;
		this.armor = cardxml.Armor;
		this.agility = cardxml.Agility;
		this.will = cardxml.Will;
		this.traits = cardxml.Traits;
		this.text = cardxml.Text;
		this.materials = cardxml.Materials;
	}

    public void Cast(Player player)
    {
        Debug.Log("Casting");

        CardFunctionsFactory cfh = new CardFunctionsFactory(player);
        cfh.AncestralRecall();
    }
}
