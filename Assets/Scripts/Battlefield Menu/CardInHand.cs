using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class CardInHand : CardUI {
	public Text nameText, traitsText, textText;
    GameObject prefab;

    public Transform parentToReturnTo = null;
    public Transform placeholderParent = null;

	// Use this for initialization
	void Start () {
        prefab = Resources.Load("Prefabs/Battlefield/CardInHand") as GameObject;
    }

    // Update is called once per frame
    void Update () {

	}

    public void SetCard(Card card)
    {
        this.card = card;
        this.nameText.text = card.name;
        this.traitsText.text = card.traits;
        this.textText.text = card.text;
    }
}
