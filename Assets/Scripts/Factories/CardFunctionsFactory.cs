using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class CardFunctionsFactory
{
    Player player;
    GameFunctionsFactory gameFunctionsFactory = new GameFunctionsFactory();
	public CardFunctionsFactory(Player player)
    {
        this.player = player;
    }

    public void AncestralRecall()
    {
        gameFunctionsFactory.Draw(player, 3);
    } 

}
