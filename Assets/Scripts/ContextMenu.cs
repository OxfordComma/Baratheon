using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public delegate void OnClickFunction();

public class ContextMenu : MonoBehaviour
{
    public void AddContextMenuItem(OnClickFunction onClickMethod)
    {
        GameObject cmItemObj = Instantiate(Resources.Load("Prefabs/ContextMenuItem") as GameObject);
        cmItemObj.transform.SetParent(this.transform);
        cmItemObj.GetComponent<Button>().onClick.AddListener(delegate { onClickMethod(); } );
    }
	
}
