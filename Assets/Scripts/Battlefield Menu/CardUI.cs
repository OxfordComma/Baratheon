using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class CardUI : MonoBehaviour {
	public Text nameText, traitsText, textText;
    public Card card;

    public Transform parentToReturnTo = null;
    public Transform placeholderParent = null;

    GameObject placeholder = null;
    public GameObject contextMenuPrefab, contextMenuButtonPrefab;



	// Use this for initialization
	void Start () {
        contextMenuPrefab = Resources.Load("Prefabs/ContextMenu") as GameObject;

    }

    // Update is called once per frame
    void Update () {
        
        if (Input.GetMouseButtonDown(1))
        {
            if (GetCardUnderMouse() == this.transform)
            {
                GameObject cmObj = Instantiate(Resources.Load("Prefabs/ContextMenu") as GameObject);
                ContextMenu cm = cmObj.GetComponent<ContextMenu>();
                OnClickFunction ocf = new OnClickFunction(Cast);
                cm.AddContextMenuItem(ocf);
            }
        } 
        
	}

    public void SetCard(Card card)
    {
        this.card = card;
		this.nameText.text = card.Name;
		this.traitsText.text = card.Traits;
		this.textText.text = card.Text;
    }

    Transform GetCardUnderMouse()
    {
        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Vector2 mousePos = Input.mousePosition;
        Debug.DrawLine(mousePos, Vector2.zero, Color.cyan);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity);
        return hit.transform;
    }

    public void Cast()
    {
        Debug.Log("Cast");
    }


}
