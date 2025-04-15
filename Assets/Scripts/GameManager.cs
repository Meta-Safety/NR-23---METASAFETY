using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using System.Collections;
using UnityEngine.Playables;

/// <summary>
/// Controls the basics functions like a pause, resume, quit game, get the VR Controllers, notifications.
/// </summary>
/// <Author: Play2Make></Author>
public class GameManager : MonoBehaviour
{
    public Transform respawnPoint;
    public GameObject player;
    public GameObject alertPanel;
    public GameObject skipPanel;
    public TMP_Text alertText;
    public GameObject finalPanel;
    public GameObject hudPanel;
    public GameObject restartPanel;
    public GameObject pauseInfoPanel;

    public bool introFinished;
    public GameObject[] rays;
    public GameObject pausePanel;
    public GameObject controlsPanel;
    public GameObject infoPausePanel;
    public GameObject[] barriers;
    public bool isPaused = false;
    private AudioSource audioSource;
    private InputDevice controller;
    private Coroutine proximityCoroutine;
    public PlayableDirector timelineController;
    private float buttonHoldTime = 0f;
    private float requiredHoldTime = 3f;

    public bool canChangeState;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        GameObject alertPanel = GameObject.FindWithTag("AlertPanel");
        player.GetComponent<DynamicMoveProvider>().moveSpeed = 0;
        GetController();
        introFinished = false;
        canChangeState = false;
    }

    private void Update()
    {
        // Used to get the controls reference.
        if (!controller.isValid)
        {
            GetController();
        }

        // Function to pause the game and skip the intro.
        if (controller.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPrimaryButtonPressed) && isPrimaryButtonPressed)
        {

            if (!isPaused && canChangeState && introFinished)
            {
                canChangeState = false;
                PauseGame();
            }
            if (isPaused && canChangeState && introFinished)
            {
                canChangeState = false;
                ResumeGame();
            }
            if (isPrimaryButtonPressed && !introFinished)
            {
                buttonHoldTime += Time.deltaTime; 

                if (buttonHoldTime >= requiredHoldTime && !introFinished)
                {
                    SkipIntro();
                }
            }
            else
            {
                buttonHoldTime = 0f; 
            }
        }
    }

    public void SkipIntro()
    {
        introFinished = true;
        timelineController.time = timelineController.duration;
        timelineController.Evaluate();
        skipPanel.SetActive(false);
        controlsPanel.SetActive(true);
        infoPausePanel.SetActive(true);
        Invoke("ChangePauseState", 2f);
    }

    // Function to get the VR Controls
    private void GetController()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, devices);

        if (devices.Count > 0)
        {
            controller = devices[0];
        }
    }

    // Function to pause the game and open the pause panel.
    private void PauseGame()
    {
        isPaused = true;
        TurnOnRays();
        player.GetComponent<DynamicMoveProvider>().moveSpeed = 0;
        pausePanel.SetActive(true);
        pauseInfoPanel.SetActive(false);
        Invoke("ChangePauseState", 2f);
    }

    public void ChangePauseState()
    {
        canChangeState = true;
    }

    // Used to return to game after the pause.
    public void ResumeGame()
    {
        player.GetComponent<DynamicMoveProvider>().moveSpeed = 3;
        TurnOffRays();
        isPaused = false;
        pausePanel.SetActive(false);
        pauseInfoPanel.SetActive(true);
        Invoke("ChangePauseState", 2f);
    }

    // Turn off the controls Rays.
    public void TurnOffRays()
    {
        foreach (GameObject ray in rays)
        {
            ray.SetActive(false);
        }
    }

    // Turn on the controls Rays.
    public void TurnOnRays()
    {
        foreach (GameObject ray in rays)
        {
            ray.SetActive(true);
        }
    }

    public bool IsAlertActive()
    {
        return alertPanel.activeSelf;
    }

    public void PlayerMovedAway()
    {
        if (proximityCoroutine != null)
        {
            StopCoroutine(proximityCoroutine);
            proximityCoroutine = null;
        }

        alertPanel.SetActive(false);
    }

    private IEnumerator ProximityTimer()
    {
        yield return new WaitForSeconds(3f);

        // Verifica se o jogador ainda está próximo
        if (alertPanel.activeSelf)
        {
            ShowRestartPanel();
        }
    }

    private void ShowRestartPanel()
    {
        //Destroy(alertPanel);
        restartPanel.SetActive(true);
        player.GetComponent<DynamicMoveProvider>().moveSpeed = 0;
        TurnOnRays();
        player.transform.position = respawnPoint.transform.position;
        pauseInfoPanel.SetActive(false);
    }

    public void TryAgain()
    {
        TurnOffRays();
        player.GetComponent<DynamicMoveProvider>().moveSpeed = 3;
        pauseInfoPanel.SetActive(true);
    }
    // Used when the player stay close to the fires.
    public void CloseToTheFire()
    {
        if (alertPanel.activeSelf)
        {
            return;
        }

        alertPanel.SetActive(true);
        alertText.text = "Você está muito próximo do fogo! Se afaste imediatamente!";

        if (proximityCoroutine == null)
        {
            proximityCoroutine = StartCoroutine(ProximityTimer());
        }
    }

    // Called when the player finish the task.
    public void NextStep()
    {
        alertPanel.SetActive(true);
        alertText.text = "Avance para o próximo desafio!";
        Invoke("TurnOffNotification", 3f);
    }

    // Called when the player use a wrong extinguisher.
    public void WrongExtinguisherNotification()
    {
        if (alertPanel.activeSelf)
        {
            return;
        }
        alertPanel.SetActive(true);
        alertText.text = "Você está utilizando o extintor errado, volte e troque pelo correto!";
        Invoke("TurnOffNotification", 3f);
    }

    // Turn the notification panel off.
    public void TurnOffNotification()
    {
        alertPanel.SetActive(false);
        hudPanel.SetActive(true);
    }

    // Turn the player able to walk.
    public void MoveOn()
    {
        player.GetComponent<DynamicMoveProvider>().moveSpeed = 2;
    }

    // Used to finish the scene, activating the final panel.
    public void FinishGame()
    {
        TurnOnRays();
        finalPanel.SetActive(true);
        player.GetComponent<DynamicMoveProvider>().moveSpeed = 0;
    }

    // Used to load a especific scene.
    public void LoadFinalScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    // Used to close the game.
    public void QuitGame()
    {
        Application.Quit();
    }

    // Used to reset the current scene.
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
