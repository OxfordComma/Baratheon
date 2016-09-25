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
        deckListItemPrefab = Resources.Load("Prefabs/Deckbuilding/DeckListItem") as GameObject;
    }

    public override void SetCard(Card card)
    {
        if (nameObj != null)
            this.nameObj.text = card.name;
        if (typeObj != null)
            this.typeObj.text = card.type;
        if (subtypeObj != null)
            this.subtypeObj.text = card.subtype;
        if (elementObj != null)
            this.elementObj.text = card.element;
        if (strengthObj != null)
            this.strengthObj.text = card.strength;
        if (armorObj != null)
            this.armorObj.text = card.armor;
        if (agilityObj != null)
            this.agilityObj.text = card.agility;
        if (willObj != null)
            this.willObj.text = card.will;
        if (traitsObj != null)
            this.traitsObj.text = card.traits;
        if (textObj != null)
            this.textObj.text = card.text;
        if (materialsObj != null)
            this.materialsObj.text = card.materials;

        this.card = card;
    }


    public override void OnClick()
    {
        GameObject deckListItem = Instantiate(deckListItemPrefab);
        deckListItem.transform.SetParent(deckList.transform);
        deckListItem.GetComponent<DeckListItem>().SetCard(card);
		GameController.GetLocalPlayer().CmdAddCardToDeck(card);
		GameController.GetLocalPlayer().CmdSaveToXML();

		GameObject.Find("CardCounter").GetComponent<Text>().text = 
			GameController.GetLocalPlayer().deckSyncList.Count.ToString() + "/30";

    }
}
