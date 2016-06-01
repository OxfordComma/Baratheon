using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("DeckCollection")]
public class DeckContainer
{
    [XmlArray("Deck"), XmlArrayItem("card")]
    public Card[] cards;

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(DeckContainer));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static DeckContainer Load(string path)
    {
        var serializer = new XmlSerializer(typeof(DeckContainer));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as DeckContainer;
        }
    }

    //Loads the xml directly from the given string. Useful in combination with www.text.
    public static DeckContainer LoadFromText(string text)
    {
        var serializer = new XmlSerializer(typeof(DeckContainer));
        return serializer.Deserialize(new StringReader(text)) as DeckContainer;
    }
}

