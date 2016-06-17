using UnityEngine;
using System.Collections;

public class InitializeHandlers : MonoBehaviour {
    GameObject playerHandlerObj, cardHandlerObj, loginHandlerObj, menuHandlerObj;

    // Use this for initialization
    void Start () {
        if (GameObject.Find("Player Handler") == null)
        {
            playerHandlerObj = new GameObject("Player Handler");
            playerHandlerObj.AddComponent<PlayerHandler>();
            DontDestroyOnLoad(playerHandlerObj);
        }

        if (GameObject.Find("Card Handler") == null)
        {
            cardHandlerObj = new GameObject("Card Handler");
            cardHandlerObj.AddComponent<CardHandler>();
            DontDestroyOnLoad(cardHandlerObj);
        }

        if (GameObject.Find("Login Handler") == null)
        {
            loginHandlerObj = new GameObject("Login Handler");
            loginHandlerObj.AddComponent<LoginHandler>();
            DontDestroyOnLoad(loginHandlerObj);
        }
        if (GameObject.Find("Menu Handler") == null)
        {
            menuHandlerObj = new GameObject("Menu Handler");
            menuHandlerObj.AddComponent<MenuHandler>();
            DontDestroyOnLoad(menuHandlerObj);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
