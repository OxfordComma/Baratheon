using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public delegate void OnClickFunction(Player player);

public class ContextMenu : MonoBehaviour
{
    public void AddContextMenuItem(string buttonName, Player player, Card card)
    {
        GameObject cmItemObj = Instantiate(Resources.Load("Prefabs/ContextMenuItem") as GameObject);
        cmItemObj.transform.SetParent(this.transform);
        
        cmItemObj.GetComponent<Button>().onClick.AddListener(delegate { card.Cast(player); } );
        cmItemObj.GetComponentInChildren<Text>().text = buttonName;
        
    }

    public void TestFunction()
    {
        Debug.Log("Test");
    }
	
}
