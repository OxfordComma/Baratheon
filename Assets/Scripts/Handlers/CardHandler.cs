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
		allCards = (Set.Load (Application.streamingAssetsPath + "/Cards.xml") as Set).set;
    }
}
