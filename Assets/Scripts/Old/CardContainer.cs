using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("CardCollection")]
public class CardContainer
{
    [XmlArray("Cards"), XmlArrayItem("card")]
    public Card[] cards;

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(CardContainer));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static CardContainer Load(string path)
    {
        var serializer = new XmlSerializer(typeof(CardContainer));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as CardContainer;
        }
    }

    //Loads the xml directly from the given string. Useful in combination with www.text.
    public static CardContainer LoadFromText(string text)
    {
        var serializer = new XmlSerializer(typeof(CardContainer));
        return serializer.Deserialize(new StringReader(text)) as CardContainer;
    }
}

