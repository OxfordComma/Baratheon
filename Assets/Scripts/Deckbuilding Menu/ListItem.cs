using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ListItem : MonoBehaviour
{
    public Card card;
    protected GameObject setList, deckList;
	public Player localPlayer;

    // Use this for initialization
    public virtual void Start()
    {
		deckList = GameObject.Find ("DeckListContent");
		setList = GameObject.Find ("SetListContent");
		localPlayer = GameController.GetGameController().localPlayer;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void SetCard(Card card) { }

    public virtual void OnClick()
    {
        Debug.Log("click");
    }

	void AddCardToDeck(Card card)
	{
		
	}

    void RemoveCardFromDeck(Card card)
    {
        
    }

}
