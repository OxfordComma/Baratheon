using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("set")]
public class Set : CardGroup
{
	[XmlElement("card")]
	public List<Card> set;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public Set()
	{
		this.set = new List<Card> ();
	}

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
}

