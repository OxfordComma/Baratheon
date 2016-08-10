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
        localPlayer.RemoveCardFromDeck(card);
        Destroy(this.gameObject);
        localPlayer.SaveToXML();
    }
}
