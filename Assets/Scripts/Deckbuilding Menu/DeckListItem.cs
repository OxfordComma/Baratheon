using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeckListItem : ListItem {

    public Text nameObj;
    public Text elementObj;

    public override void SetCard(Card card)
    {
        if (nameObj != null)
            this.nameObj.text = card.Name;
        if (elementObj != null)
            this.elementObj.text = card.Element;

        this.card = card;
    }

    public override void OnClick()
    {
        currentPlayer.RemoveCardFromDeck(card);
        Destroy(this.gameObject);
        currentPlayer.SaveToXML();
    }
}
