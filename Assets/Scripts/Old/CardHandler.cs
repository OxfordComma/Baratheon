using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class CardHandler : MonoBehaviour {
	public List<Card> allCards;

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        ImportCards();
    }

    public void ImportCards()
    {
		SetXML setxml = SetXML.Load (Application.streamingAssetsPath + "/Cards.xml");
		allCards = new Set(setxml).cards;
    }
}
