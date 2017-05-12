using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("set")]
public class SetXML : CardGroupXML
{
	[XmlElement("card")]
	public List<CardXML> cards;
	public SetXML()
	{
		this.cards= new List<CardXML> ();
	}

	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(SetXML));
		using (var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}

	public static SetXML Load(string path)
	{
		var serializer = new XmlSerializer(typeof(SetXML));
		using (var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as SetXML;
		}
	}
}

