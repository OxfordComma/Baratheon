using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameplayHandler : MonoBehaviour
{
    Player currentPlayer;
    Deck playerDeck;

    GameObject cardInHandPrefab, contextMenuPrefab;
    public GameObject handWindow;

    GameObject cmObj;
    bool contextMenuOpen = false;

    GameFunctionsHandler gameFunctionsHandler = new GameFunctionsHandler();


    // Use this for initialization
    void Start()
    {
        currentPlayer = GameObject.Find("Player Handler").GetComponent<PlayerHandler>().currentPlayer;
        contextMenuPrefab = Resources.Load("Prefabs/ContextMenu") as GameObject;
        cardInHandPrefab = Resources.Load("Prefabs/Battlefield/CardUI") as GameObject;

        playerDeck = currentPlayer.deck;
        playerDeck.Shuffle();

        gameFunctionsHandler.Draw(currentPlayer, 7);      

    }

    // Update is called once per frame
    void Update()
    {
        // Left Click
        if (Input.GetMouseButtonDown(0) && contextMenuOpen)
        {
            contextMenuOpen = false;
            //GameObject.Destroy(cmObj);
        }

        // Right Click
        if (Input.GetMouseButtonDown(1))
        {
            if (!contextMenuOpen)
            {
                Transform cardUnderMouse = gameFunctionsHandler.GetCardUnderMouse();
                cmObj = GameObject.Instantiate(contextMenuPrefab);
                cmObj.transform.SetParent(cardUnderMouse.transform);
                
                ContextMenu cm = cmObj.GetComponent<ContextMenu>();
                Debug.Log(cardUnderMouse.GetComponent<CardUI>().card.Name);
                cm.AddContextMenuItem("Cast", currentPlayer, cardUnderMouse.GetComponent<CardUI>().card);
                cmObj.transform.position = Input.mousePosition + 
                    new Vector3(cmObj.GetComponentInChildren<LayoutElement>().minWidth / 2, -cmObj.GetComponentInChildren<LayoutElement>().minHeight / 2, 0);
                contextMenuOpen = true;
                
            }
        }
        
    }

    
}
