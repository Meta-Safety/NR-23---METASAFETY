using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Used to load scenes.
/// </summary>
/// <Author: Play2Make></Author>
public class SceneController : MonoBehaviour
{
    // Load the scene by index.
    public void LoadSceneByIndex(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
