using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Make the scene transition
/// </summary>
/// <Author: Play2Make></Author>
public class SceneTransitionManager : MonoBehaviour
{

    public FadeScreen fadeScreen;

    // Set the next scene to go.
    public void GoToSceneAsync(string sceneName)
    {
        StartCoroutine(GoToSceneAsyncRoutine(sceneName));
    }

    // Start the coroutine to change de scene async.
    private IEnumerator GoToSceneAsyncRoutine(string sceneName)
    {
        if (fadeScreen != null)
        {
            fadeScreen.FadeOut();
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float timer = 0;
        while (timer <= fadeScreen.fadeDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        operation.allowSceneActivation = true;
    }

}
