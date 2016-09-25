using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    [MenuItem("Scenes/Open Main Scene %l")]
    static void OpenMainScene()
    {
        if (EditorApplication.isPlaying == true)
        {
            EditorApplication.isPlaying = false;
            return;
        }
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/Login Screen.unity");
        EditorApplication.isPlaying = true;
    }
}