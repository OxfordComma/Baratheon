using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneLoader : MonoBehaviour
{

	[MenuItem("Scenes/Open Scene 1 &1")]
    static void OpenScene1()
    {
        if (EditorApplication.isPlaying == true)
        {
            EditorApplication.isPlaying = false;
            return;
        }
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/Login Screen.unity");
//        EditorApplication.isPlaying = true;
    }

	[MenuItem("Scenes/Open Scene 2 &2")]
	static void OpenScene2()
	{
		if (EditorApplication.isPlaying == true)
		{
			EditorApplication.isPlaying = false;
			return;
		}
		EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
		EditorSceneManager.OpenScene("Assets/Scenes/Main Menu.unity");
//		EditorApplication.isPlaying = true;
	}

	[MenuItem("Scenes/Open Scene 3 &3")]
	static void OpenScene3()
	{
		if (EditorApplication.isPlaying == true)
		{
			EditorApplication.isPlaying = false;
			return;
		}
		EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
		EditorSceneManager.OpenScene("Assets/Scenes/Build Deck.unity");
//		EditorApplication.isPlaying = true;
	}

	[MenuItem("Scenes/Open Scene 4 &4")]
	static void OpenScene4()
	{
		if (EditorApplication.isPlaying == true)
		{
			EditorApplication.isPlaying = false;
			return;
		}
		EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
		EditorSceneManager.OpenScene("Assets/Scenes/Battlefield.unity");
//		EditorApplication.isPlaying = true;
	}
}