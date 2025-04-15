using System.Collections;
using UnityEngine;

/// <summary>
/// Used to make the fadescreen effects.
/// </summary>
/// <Author: Play2Make></Author>
public class FadeScreen : MonoBehaviour
{

    public bool fadeOnStart = true;

    public float fadeDuration = 2;

    public Color fadeColor;

    private Renderer rend;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        if (fadeOnStart)
        {
            FadeIn();
        }
    }

    // Used to start the fade effect.
    public void FadeIn()
    {
        Fade(1,0);
    }

    // Used to stop de fade effect.
    public void FadeOut()
    {
            Fade(0,1);
    }

    // Called to make de fade effect.
    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    // Coroutine to start and finish the fade effect.
    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;
        while (timer <= fadeDuration)
        {

            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);
            rend.material.SetColor("_Color", newColor);
            
            timer += Time.deltaTime;
            yield return null;
        }
        
        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;
        rend.material.SetColor("_Color", newColor2);
    }
}
