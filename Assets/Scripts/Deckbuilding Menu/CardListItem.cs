using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardListItem : MonoBehaviour
{
	public GameObject cardListItemPrefab;
	GameObject setList, deckList;
	Player currentPlayer;

    public Text nameObj;
    public Text typeObj;
    public Text subtypeObj;
    public Text elementObj;
    public Text strengthObj;
    public Text armorObj;
    public Text agilityObj;
    public Text willObj;
    public Text traitsObj;
    public Text textObj;
    public Text materialsObj;
    public Text recipeObj;
    public Card card;

    // Use this for initialization
    void Start()
    {
		deckList = GameObject.Find ("DeckListContent");
		setList = GameObject.Find ("SetListContent");

		currentPlayer = GameObject.Find ("Player Handler").GetComponent<PlayerHandler>().currentPlayer;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void SetCard(Card card)
    {
        this.nameObj.text = card.Name;
        this.typeObj.text = card.Type;
        this.subtypeObj.text = card.Subtype;
        this.elementObj.text = card.Element;
        this.strengthObj.text = card.Strength;
        this.armorObj.text = card.Armor;
        this.agilityObj.text = card.Agility;
        this.willObj.text = card.Will;
        this.traitsObj.text = card.Traits;
        this.textObj.text = card.Text;
        this.materialsObj.text = card.Materials;

        this.card = card;

    }

	public void OnClick()
	{
        if (this.transform.parent.name == deckList.name)
            RemoveCardFromDeck(this.card);
        else if (this.transform.parent.name == setList.name)
            if (currentPlayer.CanAddCardToDeck(this.card))
                AddCardToDeck(this.card);
    }

	void AddCardToDeck(Card card)
	{
		GameObject clip = Instantiate (cardListItemPrefab);
		clip.transform.SetParent (deckList.transform);
        clip.GetComponent<CardListItem>().SetCard(card);
		currentPlayer.AddCardToDeck (card);
		currentPlayer.SaveToXML ();
	}

    void RemoveCardFromDeck(Card card)
    {
        currentPlayer.RemoveCardFromDeck(card);
        Destroy(this.gameObject);
        currentPlayer.SaveToXML();
    }

}
