using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Used to controls the scene change.
/// </summary>
/// <Author: Play2Make></Author>
public class TimelineController : MonoBehaviour
{
    SceneTransitionManager sceneTransitionManager;

    private void OnEnable()
    {
        // When enable load the practiveRoom scene.
        sceneTransitionManager = FindFirstObjectByType<SceneTransitionManager>();
        SceneManager.LoadScene("practiceRoom"); 
    }
}
