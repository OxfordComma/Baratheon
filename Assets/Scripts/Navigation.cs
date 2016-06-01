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

	public Navigation()
	{
	}

    public void GoToDeckbuilding()
    {
        SceneManager.LoadScene("Build Deck");
    }

	public static void StaticGoToMainMenu()
	{
		SceneManager.LoadScene("Main Menu");
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
	}

	public void Logout()
	{
		PlayerHandler playerHandler = GameObject.Find("Player Handler").GetComponent<PlayerHandler>();
		playerHandler.currentPlayer.SaveToXML();
		playerHandler.currentPlayer = null;
		GoToLoginScreen();
	}


	public void Quit()
	{
		Application.Quit ();
	}
}
