using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public delegate void OnClickFunction(Player player);

public class BattlefieldContextMenu : MonoBehaviour
{
    public void AddContextMenuItem(string buttonName, NetworkPlayer player, Card card)
    {
        GameObject cmItemObj = Instantiate(Resources.Load("Prefabs/ContextMenuItem") as GameObject);
        cmItemObj.transform.SetParent(this.transform);
		Button cmItemObjButton = cmItemObj.GetComponent<Button> ();
        cmItemObjButton.onClick.AddListener(delegate { 
			card.Cast(player); 
		} );
		cmItemObjButton.onClick.AddListener (delegate {
			Destroy(cmItemObj);
		});
        cmItemObj.GetComponentInChildren<Text>().text = buttonName;
        
    }
}

