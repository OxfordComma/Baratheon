using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class CardFunctionsHandler
{
    Player player;
    GameFunctionsHandler gameFunctionsHandler = new GameFunctionsHandler();
    public CardFunctionsHandler(Player player)
    {
        this.player = player;
    }

    public void AncestralRecall()
    {
        gameFunctionsHandler.Draw(player, 3);
    } 

}
