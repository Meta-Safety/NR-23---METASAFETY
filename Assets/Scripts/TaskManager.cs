using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls all tasks on practice room.
/// </summary>
/// <Author: Play2Make></Author>
public class TaskManager : MonoBehaviour
{
    GameManager gameManager;
    public List<ScriptableMission> missions;
    public AudioSource audioSource;
    public List<GameObject> barriers; // Lista de barreiras para desativar.
    public List<GameObject> missionEnvironment; // Lista de ambientes da miss�o para ativar.

    private int currentMissionIndex = 0;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Start()
    {
        // On start, reset the tasks status.
        ResetMissionStatus();

        if (missions != null && missions.Count > 0)
        {
            LoadMission(currentMissionIndex);
        }
        else
        {
            Debug.LogWarning("A lista de miss�es est� vazia ou n�o foi atribu�da.");
        }
    }
    // Check every frame when the task is completed.
    void Update()
    {
        if (missions != null && currentMissionIndex < missions.Count)
        {
            if (missions[currentMissionIndex].isCompleted)
            {
                CompleteCurrentMission();
                GoToNextMission();
            }
        }
        else
        {
            Debug.LogWarning("O �ndice da miss�o atual est� fora do alcance ou a lista de miss�es � nula.");
        }
    }

    // Reset all tasks to not completed.
    private void ResetMissionStatus()
    {
        if (missions != null)
        {
            foreach (var mission in missions)
            {
                if (mission != null)
                {
                    mission.isCompleted = false;
                }
            }
        }
        else
        {
            Debug.LogWarning("A lista de miss�es est� nula ao tentar redefinir o status.");
        }
    }

    // Complete the current task and change the infos.
    public void CompleteCurrentMission()
    {
        if (missions != null && currentMissionIndex < missions.Count)
        {
            missions[currentMissionIndex].isCompleted = true;
            Debug.Log("Fez quest de " + missions[currentMissionIndex].missionDescription);

            // Desativa a barreira correspondente.
            if (barriers != null && currentMissionIndex < barriers.Count)
            {
                if (barriers[currentMissionIndex] != null)
                {
                    barriers[currentMissionIndex].SetActive(false);
                }
                else
                {
                    Debug.LogWarning("A barreira no �ndice especificado � nula.");
                }
            }

            // Ativa o ambiente correspondente.
            if (missionEnvironment != null && currentMissionIndex < missionEnvironment.Count)
            {
                if (missionEnvironment[currentMissionIndex] != null)
                {
                    missionEnvironment[currentMissionIndex].SetActive(true);
                }
                else
                {
                    Debug.LogWarning("O ambiente no �ndice especificado � nulo.");
                }
            }

            gameManager.NextStep();
        }
        else
        {
            Debug.LogWarning("N�o foi poss�vel completar a miss�o: �ndice fora do alcance ou lista nula.");
        }
    }

    // Used to load the next mission, setting up the informations.
    public void LoadMission(int missionIndex)
    {
        if (missions != null && missionIndex < missions.Count)
        {
            ScriptableMission mission = missions[missionIndex];

            if (mission.missionAudio != null)
            {
                audioSource.clip = mission.missionAudio;
                audioSource.Play();
            }
        }
    }

    // When the task is done, check if is the last one, else if load the next mission.
    public void GoToNextMission()
    {
        currentMissionIndex++;
        if (missions != null && currentMissionIndex < missions.Count)
        {
            LoadMission(currentMissionIndex);
        }
        else
        {
            gameManager.FinishGame();
        }
    }
}
