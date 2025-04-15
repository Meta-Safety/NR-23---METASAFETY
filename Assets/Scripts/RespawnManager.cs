using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

/// <summary>
/// Controls the basics functions like a pause, resume, quit game, get the VR Controllers, notifications.
/// </summary>
/// <Author: Play2Make></Author>
public class RespawnManager : MonoBehaviour
{
    public Transform respawnPoint;
    private AudioSource audioSource;
    public GameObject WithFireBoxes, WithFireMachines, WithFireOils;
    public GameObject WithoutFireBoxes, WithoutFireMachines, WithoutFireOils;
    public GameObject player;
    public GameObject alertPanel;
    public TMP_Text alertText;
    public GameObject pausePanel;
    public GameObject startPanel;
    public GameObject restartPanel;
    public GameObject pauseInfoPanel;
    public GameObject hudPanel;
    public bool isPaused;

    public GameObject[] rays;

    public int objectivesDone;

    public GameObject finalPanel;

    private InputDevice controller;

    private List<GameObject> fireObjects;
    private List<GameObject> noFireObjects;
    private bool isInitialized = false;

    public bool canChangeState;

    private Coroutine proximityCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        objectivesDone = 0;
        audioSource = GetComponent<AudioSource>();
        player.GetComponent<DynamicMoveProvider>().moveSpeed = 0;
        GetController();
        canChangeState = false;
    }

    private void Update()
    {
        CheckObjectives();
        {
            // Used to get the controls reference.
            if (!controller.isValid)
            {
                GetController();
            }


            if (controller.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPrimaryButtonPressed) && isPrimaryButtonPressed)
            {
                if (!isPaused && canChangeState)
                {
                    canChangeState = false;
                    PauseGame();
                }
                if (isPaused && canChangeState)
                {
                    canChangeState = false;
                    ResumeGame();
                }
            }
        }
        
    }

    // Check when the player complete all tasks.
    public void CheckObjectives()
    {
        if (objectivesDone == 3)
        {
            FinishGame();
        }
    }

    // Create a list with objects with and without fire.
    private void InitializeLists()
    {
        fireObjects = new List<GameObject> { WithFireBoxes, WithFireMachines, WithFireOils };
        noFireObjects = new List<GameObject> { WithoutFireBoxes, WithoutFireMachines, WithoutFireOils };
        isInitialized = true;
    }

    public void ChangePauseState()
    {
        canChangeState = true;
    }

    // Activate a random fire object on scene.
    public void ActivateRandomFireObject()
    {
        if (!isInitialized)
        {
            InitializeLists();
        }

        int fireIndex = Random.Range(0, fireObjects.Count);
        GameObject fireObject = fireObjects[fireIndex];
        fireObject.SetActive(true);
        fireObjects.RemoveAt(fireIndex); 


        List<int> availableNoFireIndices = new List<int>();
        for (int i = 0; i < noFireObjects.Count; i++)
        {
            availableNoFireIndices.Add(i);
        }

        int noFireIndex1 = availableNoFireIndices[Random.Range(0, availableNoFireIndices.Count)];
        GameObject noFireObject1 = noFireObjects[noFireIndex1];
        noFireObject1.SetActive(true);
        availableNoFireIndices.Remove(noFireIndex1);

        int noFireIndex2 = availableNoFireIndices[Random.Range(0, availableNoFireIndices.Count)];
        GameObject noFireObject2 = noFireObjects[noFireIndex2];
        noFireObject2.SetActive(true);

        noFireObjects.Remove(noFireObject1);
        noFireObjects.Remove(noFireObject2);
    
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

    // Called when the player use a wrong extinguisher.
    public void WrongExtinguisher()
    {
        if (alertPanel.activeSelf)
        {
            return; 
        }
        alertPanel.SetActive(true);
        alertText.text = "Você está utilizando o extintor errado, troque e tente novamente";
        Invoke("TurnOffAlert", 3f);
    }

    // Turn the notification panel off.
    public void TurnOffAlert()
    {
        alertPanel.SetActive(false);
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
        // Destroy(alertPanel);
        restartPanel.SetActive(true);
        player.GetComponent<DynamicMoveProvider>().moveSpeed = 0;
        TurnOnRays();
        player.transform.position = respawnPoint.transform.position;
        pauseInfoPanel.SetActive(false);
    }

    public void TryAgain()
    {
        TurnOffRays();
        pauseInfoPanel.SetActive(true);
        player.GetComponent<DynamicMoveProvider>().moveSpeed = 3;
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

    // Used to start the game.
    public void StartGame()
    {
        TurnOffRays();       
        startPanel.SetActive(false);
        player.GetComponent<DynamicMoveProvider>().moveSpeed = 3;
        hudPanel.SetActive(true);
        ActivateRandomFireObject();
        canChangeState = true;
        //audioSource.Play();
    }

    // Function to pause the game and open the pause panel.
    public void PauseGame()
    {
        TurnOnRays();
        player.GetComponent<DynamicMoveProvider>().moveSpeed = 0;
        hudPanel.SetActive(false);
        pausePanel.SetActive(true);
        pauseInfoPanel.SetActive(false);
        isPaused = true;
        Invoke("ChangePauseState", 2f);
    }

    // Bool to check if the game is paused.
    public void CanReturnGame()
    {
        isPaused = true;
    }

    // Used to return to game after the pause.
    public void ResumeGame()
    {
        TurnOffRays();
        player.GetComponent<DynamicMoveProvider>().moveSpeed = 3;
        pausePanel.SetActive(false);
        hudPanel.SetActive(true);
        pauseInfoPanel.SetActive(true);
        isPaused = false;
        Invoke("ChangePauseState", 2f);
    }

    // Turn on the controls Rays.
    public void TurnOnRays()
    {
        foreach (GameObject ray in rays)
        {
            ray.SetActive(true);
        }
    }

    // Turn off the controls Rays.
    public void TurnOffRays()
    {
        foreach (GameObject ray in rays)
        {
            ray.SetActive(false);
        }
    }

    // Used to reset the current scene.
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Used to close the game.
    public void QuitGame()
    {
        Application.Quit();
    }

    // Used to finish the scene, activating the final panel.
    public void FinishGame()
    {
        player.GetComponent<DynamicMoveProvider>().moveSpeed = 0;
        TurnOnRays(); 
        finalPanel.SetActive(true);
    }
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
