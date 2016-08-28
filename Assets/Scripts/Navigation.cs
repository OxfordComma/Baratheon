using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;

public class Navigation : MonoBehaviour {

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

	public Navigation() { }

	public static void StaticGoToMainMenu()
	{
		SceneManager.LoadScene("Main Menu");
	}

    public void GoToDeckbuilding()
    {
        SceneManager.LoadScene("Build Deck");
    }

	public void GoToMainMenu()
	{
		SceneManager.LoadScene("Main Menu");
	}

    public void GoToLoginScreen()
    {
        SceneManager.LoadScene("Login Screen");
    }

	public void GoToBattlefield()
	{
		SceneManager.LoadScene ("Battlefield");
		Debug.Log (GameController.GetGameController ().localPlayer);

	}

	public void Logout()
	{
		GameController.GetGameController().localPlayer.SaveToXML();
		GameController.GetGameController().localPlayer = null;
		GoToLoginScreen();
	}


	public void Quit()
	{
		Application.Quit ();
	}
}
