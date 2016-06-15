using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetListItem : ListItem {
    GameObject deckListItemPrefab;

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

    public override void Start()
    {
        base.Start();
        deckListItemPrefab = Resources.Load("Prefabs/Build Deck/DeckListItem") as GameObject;
    }

    public override void SetCard(Card card)
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


    public override void OnClick()
    {
        GameObject deckListItem = Instantiate(deckListItemPrefab);
        deckListItem.transform.SetParent(deckList.transform);
        deckListItem.GetComponent<DeckListItem>().SetCard(card);
        currentPlayer.AddCardToDeck(card);
        currentPlayer.SaveToXML();
    }
}
