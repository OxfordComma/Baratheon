using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;

//	Caps for XML formatting
public class CardXML {
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

	public CardXML() { }

	public CardXML(string name)
	{
		this.Name = name;
	}

	public Card ToCard()
	{
		return new Card (this);
	}
}
