using UnityEngine;

/// <summary>
/// Used to controls the missions barriers.
/// </summary>
/// <Author: Play2Make></Author>
public class BarrierController : MonoBehaviour
{
    public Animator barrierAnimator;
    public GameObject firstMissionWay;
    public GameManager gameManager;
    public GameObject firstPanel;
    public GameObject secondPanel;

    private void OnEnable()
    {
        OpenGate();
    }

    // Open the barrier to next mission.
    public void OpenGate()
    {
        firstMissionWay.SetActive(true);
        barrierAnimator.enabled = true;
        barrierAnimator.SetBool("openbarrier", true);
        Invoke("ChangeState", 2f);
    }

    public void ChangeState()
    {
        gameManager.canChangeState = true;
        gameManager.introFinished = true;
        firstPanel.SetActive(false);
        secondPanel.SetActive(true);
    }
}
