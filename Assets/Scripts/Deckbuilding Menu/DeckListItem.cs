using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeckListItem : ListItem {

    public Text nameObj;
    public Text elementObj;

    public override void SetCard(Card card)
    {
        if (nameObj != null)
            this.nameObj.text = card.name;
        if (elementObj != null)
            this.elementObj.text = card.element;

        this.card = card;
    }

    public override void OnClick()
    {
		GameController.GetLocalPlayer().CmdRemoveCardFromDeck(card);
		GameController.GetLocalPlayer().SaveToXML();
		GameObject.Find("CardCounter").GetComponent<Text>().text = 
			GameController.GetLocalPlayer().syncListStringDeck.Count.ToString() + "/30";
		
        Destroy(this.gameObject);
		GameController.GetLocalPlayer().SaveToXML();
    }
}
