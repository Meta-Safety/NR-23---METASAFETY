using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Tracks the time the player spends in the current scene and logs
/// whether the user skipped or rewound a video timeline.
/// Also saves this information using PlayerPrefs.
/// </summary>
/// <Author>Play2Make</Author>
public class TimelineTracker : MonoBehaviour
{
    private float timeSpentInScene;     // Total time the user has spent in the current scene
    private string sceneKey;            // PlayerPrefs key to store scene time
    private string skippedKey;          // PlayerPrefs key to mark skip/rewind action

    [Header("Timeline Settings")]
    public PlayableDirector videoDirector; // Reference to the timeline director component
    public bool isVideoScene;              // Defines whether the scene contains video playback

    private void Start()
    {
        timeSpentInScene = 0f;

        // Optionally, you can set the sceneKey and skippedKey dynamically here if needed
        // For example:
        // sceneKey = $"TimeSpent_{SceneManager.GetActiveScene().name}";
        // skippedKey = $"Skipped_{SceneManager.GetActiveScene().name}";
    }

    private void Update()
    {
        // Accumulate time spent in the scene (per frame)
        timeSpentInScene += Time.deltaTime;
    }

    /// <summary>
    /// Called when the user skips or rewinds the timeline content.
    /// Marks this behavior in PlayerPrefs.
    /// </summary>
    public void OnSkipOrRewind()
    {
        PlayerPrefs.SetInt(skippedKey, 1);
        PlayerPrefs.Save();
        Debug.Log($"{skippedKey} registered as skipped.");
    }

    private void OnDestroy()
    {
        // Save the total time spent in this scene when the object is destroyed (e.g., on scene unload)
        PlayerPrefs.SetFloat(sceneKey, timeSpentInScene);
        PlayerPrefs.Save();
        Debug.Log($"Time saved: {sceneKey} = {timeSpentInScene:F2} seconds");
    }
}
