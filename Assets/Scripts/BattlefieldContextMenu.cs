using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public delegate void OnClickFunction(Player player);

public class BattlefieldContextMenu : MonoBehaviour
{
    public void AddContextMenuItem(string buttonName, NetworkPlayer player, Card card)
    {
        GameObject cmItem = Resources.Load("Prefabs/ContextMenuItem") as GameObject;
        GameObject cmItemObj = Instantiate(cmItem);
        GameObject cmItemParentObj = this.transform.gameObject;

        cmItemObj.transform.SetParent(this.transform);
        Button cmObjButton = cmItemObj.GetComponent<Button> ();
        cmObjButton.onClick.AddListener(delegate {
            Destroy(cmItemParentObj);
        });
        cmObjButton.onClick.AddListener(delegate { 
			card.Cast(player); 
		} );

        cmItemObj.GetComponentInChildren<Text>().text = buttonName;
        
    }
}

