using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.Playables;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using System.Collections.Generic;
using UnityEngine.Rendering;
/// <summary>
/// Used to manipulate the video controller and the timeline.
/// </summary>
/// <Author: Play2Make></Author>
public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public PlayableDirector playableDirector; // Referência para a Timeline
    public float jumpTime = 5f;

    public bool isPaused;
    public bool canChangeState;

    public GameObject pausePanel;
    public GameObject pauseInfoPanel;

    private InputDevice controller;

    private void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
        }
        canChangeState = true;
    }

    private void Update()
    {
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

    public void PauseGame()
    {
        if (playableDirector != null)
        {
            playableDirector.Pause();
        }

        if (videoPlayer != null)
        {
            videoPlayer.Pause();
        }
        isPaused = true;
        pausePanel.SetActive(true);
        pauseInfoPanel.SetActive(false);
        Invoke("ChangePauseState", 2f);
    }

    public void ResumeGame()
    {
        if (playableDirector != null)
        {
            playableDirector.Resume();
        }

        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }
        isPaused = false;
        pausePanel.SetActive(false);
        pauseInfoPanel.SetActive(true);
        Invoke("ChangePauseState", 2f);
    }

    public void ChangePauseState()
    {
        canChangeState = true;
    }

    private void GetController()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, devices);

        if (devices.Count > 0)
        {
            controller = devices[0];
        }
    }

    // Used to start the video player.
    public void PlayClip(VideoClip currentClip)
    {
        videoPlayer.clip = currentClip;
        videoPlayer.Play();
        Debug.Log("Video on");
    }

    // When the video end, load the ContentRoom Scene.
    public void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("ContentRoom");
    }

    // Advance the video forward.
    public void AdvanceVideo()
    {
        if (videoPlayer.canSetTime)
        {
            videoPlayer.time = Mathf.Min((float)videoPlayer.time + jumpTime, (float)videoPlayer.length);
        }
    }

    // Return the video back.
    public void RewindVideo()
    {
        if (videoPlayer.canSetTime)
        {
            videoPlayer.time = Mathf.Max((float)videoPlayer.time - jumpTime, 0);
        }
    }

    // Advance the TimeLine forward.
    public void AdvanceTimeline()
    {
        if (playableDirector != null)
        {
            double newTime = playableDirector.time + jumpTime;
            playableDirector.time = Mathf.Min((float)newTime, (float)playableDirector.duration); // Garante que não ultrapasse a duração
        }
    }

    // Return the TimeLine back.
    public void RewindTimeline()
    {
        if (playableDirector != null)
        {
            double newTime = playableDirector.time - jumpTime;
            playableDirector.time = Mathf.Max((float)newTime, 0); // Garante que não volte para antes do início
        }
    }
}
