using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameplayHandler : MonoBehaviour
{
    Player currentPlayer;
    Deck playerDeck;
    public GameObject cardUIPrefab;
    public GameObject handWindow;

	Player whosTurnIsIt;

    // Use this for initialization
    void Start()
    {
		cardUIPrefab = Resources.Load ("Prefabs/CardUI") as GameObject;
        currentPlayer = GameObject.Find("Player Handler").GetComponent<PlayerHandler>().currentPlayer;
        playerDeck = currentPlayer.deck;

        playerDeck.Shuffle();


        for (int i = 0; i < 7; i++)
        {
            Draw();            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.DrawLine(mousePos, Vector2.zero, Color.cyan);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity);
            //Debug.Log(hit);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.tag);
            }
            else
            {
                Debug.Log("nothing");
            }
            
        }
    }

    public void Draw()
    {
        Card card = playerDeck.cards[0];
        playerDeck.cards.RemoveAt(0);
        GameObject cardUIToAdd = Instantiate(cardUIPrefab);
        cardUIToAdd.GetComponent<CardUI>().SetCard(card);
		cardUIToAdd.transform.SetParent(handWindow.transform, false);
    }
}
