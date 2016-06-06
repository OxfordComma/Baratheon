using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class CardUI : MonoBehaviour {
	public Text nameText, traitsText, textText;
    Card card;

    public Transform parentToReturnTo = null;
    public Transform placeholderParent = null;
    GameObject placeholder = null;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetCard(Card card)
    {
        this.card = card;
		this.nameText.text = card.Name;
		this.traitsText.text = card.Traits;
		this.textText.text = card.Text;
    }


}
