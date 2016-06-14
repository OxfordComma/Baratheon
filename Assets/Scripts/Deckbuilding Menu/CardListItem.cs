using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardListItem : MonoBehaviour
{
	public GameObject cardListItemPrefab;
	public GameObject setList, deckList;
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
        if (nameObj != null)
            this.nameObj.text = card.Name;
        if (typeObj != null)
            this.typeObj.text = card.Type;
        if (subtypeObj != null)
            this.subtypeObj.text = card.Subtype;
        if (elementObj != null)
            this.elementObj.text = card.Element;
        if (strengthObj != null)
            this.strengthObj.text = card.Strength;
        if (armorObj != null)
            this.armorObj.text = card.Armor;
        if (agilityObj != null)
            this.agilityObj.text = card.Agility;
        if (willObj != null)
            this.willObj.text = card.Will;
        if (traitsObj != null)
            this.traitsObj.text = card.Traits;
        if (textObj != null)
            this.textObj.text = card.Text;
        if (materialsObj != null)
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
