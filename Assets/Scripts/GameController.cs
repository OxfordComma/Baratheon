using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
	[HideInInspector]
	public Player localPlayer;
	public Set set; 

    GameObject cardInHandPrefab, contextMenuPrefab;
	GameObject escapeMenuPrefab;
    public GameObject handWindow;

    GameObject cmObj;
	bool contextMenuOpen = false;

	GameFunctionsFactory gameFunctionsFactory = new GameFunctionsFactory();

	public List<Card> AllCards
	{
		get { return set.cards; }
	}

    // Use this for initialization
    void Start()
    {
		DontDestroyOnLoad (this);

		set = new Set(SetXML.Load (Application.streamingAssetsPath + "/Cards.xml"));

        contextMenuPrefab = Resources.Load("Prefabs/ContextMenu") as GameObject;
        cardInHandPrefab = Resources.Load("Prefabs/Battlefield/CardUI") as GameObject;
		escapeMenuPrefab = Resources.Load ("Prefabs/EscapeMenu") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Left Click
		if (Input.GetMouseButtonDown(0))
        {

        }

        // Right Click
        if (Input.GetMouseButtonDown(1))
        {
			// Context Menu
            if (!contextMenuOpen)
            {
				Transform cardUnderMouse = gameFunctionsFactory.GetObjectUnderMouse().transform;
                cmObj = GameObject.Instantiate(contextMenuPrefab);
				cmObj.transform.SetParent(GameObject.Find("Canvas").transform);
				cmObj.transform.SetAsLastSibling ();
                
                BattlefieldContextMenu cm = cmObj.GetComponent<BattlefieldContextMenu>();
                Debug.Log(cardUnderMouse.GetComponent<CardUI>().card.name);
                cm.AddContextMenuItem("Cast", localPlayer, cardUnderMouse.GetComponent<CardUI>().card);
                cmObj.transform.position = Input.mousePosition + 
                    new Vector3(cmObj.GetComponentInChildren<LayoutElement>().minWidth / 2, -cmObj.GetComponentInChildren<LayoutElement>().minHeight / 2, 0);
                
            }
        }

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!GameObject.Find ("EscapeMenu")) {
				GameObject escapeMenuObj = Instantiate (escapeMenuPrefab);
				escapeMenuObj.name = "EscapeMenu";
				escapeMenuObj.transform.SetParent (GameObject.Find ("Canvas").transform);
				escapeMenuObj.transform.position = new Vector3 (Screen.width / 2, Screen.height / 2);
				escapeMenuObj.GetComponentInChildren<Button> ().onClick.AddListener (delegate {
					Destroy (escapeMenuObj);
				});
			} else {
				Destroy (GameObject.Find ("EscapeMenu"));
			}
		}
        
    }

	public static GameController GetGameController()
	{
		return GameObject.FindWithTag ("GameController").GetComponent<GameController> ();
	}

	void OnLevelWasLoaded(int level)
	{
		if (level == 3) // Battlefield
		{
			localPlayer.deck.Shuffle();
			gameFunctionsFactory.Draw(localPlayer, 7);   
		}
	}

	// Scene 0
	public static void GoToLoginScreen()
	{
		SceneManager.LoadScene("Login Screen");
	}

	// Scene 1
	public static void GoToMainMenu()
	{
		SceneManager.LoadScene("Main Menu");
	}

	// Scene 2
	public static void GoToDeckbuilding()
	{
		SceneManager.LoadScene("Build Deck");
	}

	// Scene 3
	public static void GoToBattlefield()
	{
		SceneManager.LoadScene ("Battlefield");
	}


	public void Quit()
	{
		Application.Quit ();
	}

    
}
