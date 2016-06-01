using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CardManager : MonoBehaviour {
    public List<Card> allCards { get; set; }

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        allCards = new List<Card>(CardContainer.Load(Path.Combine(Application.dataPath, "cards.xml")).cards);
    }

    public void ImportCards()
    {
        allCards = new List<Card>(CardContainer.Load(Path.Combine(Application.dataPath, "cards.xml")).cards);
    }
}
