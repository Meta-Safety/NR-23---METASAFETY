using UnityEngine;
using DG.Tweening;

/// <summary>
/// Controls the panels in game effects, scale and position. 
/// </summary>
/// <Author: Play2Make></Author>
public class PanelsEffects : MonoBehaviour
{
    private Vector3 initialPosition;
    public bool verticalEffect;
    public bool scaleEffect;
    public bool objectsEffect;

    void Start()
    {
        // Check the bool to start the effect.
        initialPosition = transform.position;

        if (verticalEffect)
        {
            MoveUpAndDown();
        }
        if (scaleEffect)
        {
            ScaleEffect();
        }

        if (objectsEffect)
        {
            ObjectsEffect();
        }
    }

    // Function to manage the Y position effect.
    void ObjectsEffect()
    {
        transform.DOMoveY(initialPosition.y + 0.50f, 1f)
    .SetEase(Ease.InOutSine)
    .OnComplete(() =>
    {
        transform.DOMoveY(initialPosition.y - 0.50f, 1f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => ObjectsEffect());
    });
    }

    // Function to manage the Y position effect.
    void MoveUpAndDown()
    {
        transform.DOMoveY(initialPosition.y + 0.10f, 3f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                transform.DOMoveY(initialPosition.y - 0.10f, 3f)
                    .SetEase(Ease.InOutSine)
                    .OnComplete(() => MoveUpAndDown());
            });
    }

    // Function to manage the scale effect.
    void ScaleEffect()
    {
        transform.DOScale(1.1f, 1.5f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                transform.DOScale(1.0f, 1.5f)
                    .SetEase(Ease.InOutSine)
                    .OnComplete(() => ScaleEffect());
            });
    }
}
