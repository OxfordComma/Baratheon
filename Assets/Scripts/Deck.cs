using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;

public class Deck
{
	
	public List<Card> cards;

	public Deck()
	{
		this.cards = new List<Card>();
	}

    public void AddCard(Card card)
    {
		cards.Add(card);
    }

	public void RemoveCard(Card card)
	{
		cards.Remove (card);
	}	

    public void Shuffle()
    {
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(1, n + 1);
            Card value = cards[k];
            cards[k] = cards[n];
            cards[n] = value;
        }
    }

	public string[] ToStringArray()
	{
		string[] toReturn = new string[cards.Count];
		for(int i = 0; i < cards.Count; i++)
		{
			toReturn[i] = cards[i].name;
		}
		return toReturn;
	}

	public SyncListString ToSyncListString()
	{
		SyncListString toReturn = new SyncListString ();
		for (int i = 0; i < cards.Count; i++) {
			toReturn [i] = cards [i].name;
		}
		return toReturn;
	}
}

