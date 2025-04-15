using UnityEngine;

[CreateAssetMenu(fileName = "NewMission", menuName = "Mission System/Mission")]
public class ScriptableMission : ScriptableObject
{
    public string missionName;        
    public AudioClip missionAudio;     
    public string missionDescription;

    public bool isCompleted = false;

    public bool IsMissionComplete()
    {
        return isCompleted;
    }

    public void CompleteMission()
    {
        isCompleted = true;
    }
}
