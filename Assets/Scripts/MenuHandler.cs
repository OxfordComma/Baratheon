using UnityEngine;
using System.Collections;

public class MenuHandler : MonoBehaviour {

    bool menuShowing;
    GameObject escapeMenuPrefab;
    GameObject escapeMenuInScene;

    // Use this for initialization
    void Start () {
        escapeMenuPrefab = (GameObject)Resources.Load("Prefabs/EscapeMenu");

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuShowing)
            {
                menuShowing = true;
                escapeMenuInScene = Instantiate(escapeMenuPrefab);
                escapeMenuInScene.transform.SetParent(GameObject.Find("Canvas").transform);
                escapeMenuInScene.transform.position = new Vector3(Screen.width / 2, Screen.height/2);
            }
            else
            {
                menuShowing = false;
                Destroy(escapeMenuInScene);
            }
        }
    }
}
