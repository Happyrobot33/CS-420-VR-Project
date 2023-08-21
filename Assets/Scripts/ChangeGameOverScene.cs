using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ChangeGameOverScene : MonoBehaviour
{
    public GameObject badGameEndingPropsGroup;
    public GameObject goodGameEndingPropsGroup;
    
    public Material skyboxForWinning;
    public Material skyboxForLosing;

    public TextMeshProUGUI gameOverHeaderUI;
    public TextMeshProUGUI remainingTimeUI;
    public TextMeshProUGUI targetsHitUI;

    // Start is called before the first frame update
    void Start()
    {
        int finalConvertedHours = TimeSpan.FromSeconds(GameManager.capturedTimeSeconds).Hours;
        int finalConvertedMinutes = TimeSpan.FromSeconds(GameManager.capturedTimeSeconds).Minutes;
        int finalConvertedSeconds = TimeSpan.FromSeconds(GameManager.capturedTimeSeconds).Seconds;
        int finalConvertedMilSeconds = TimeSpan.FromSeconds(GameManager.capturedTimeSeconds).Milliseconds;
        string remainingTime = string.Format("{0:00}:{1:00}:{2:00}:{3:000}", finalConvertedHours, finalConvertedMinutes, finalConvertedSeconds, finalConvertedMilSeconds);

        // Setup the scene depending on if the player won or lost:
        if (GameManager.didTheyWin)
        {
            // Skybox stuff:
            RenderSettings.skybox = skyboxForWinning;

            //Canvas UI stuff:
            gameOverHeaderUI.text = "Congrats! You WON!";
            remainingTimeUI.text = "Time Left: " + remainingTime;
            targetsHitUI.text = "Targets hit: " + GameManager.capturedTargetsHit.ToString("0");

            //Props stuff:
            badGameEndingPropsGroup.SetActive(false);
            goodGameEndingPropsGroup.SetActive(true);

        }
        else
        {
            // Skybox stuff:
            RenderSettings.skybox = skyboxForLosing;

            //Canvas UI stuff:
            gameOverHeaderUI.text = "Game Over.";
            remainingTimeUI.text = "Time ran out!";
            targetsHitUI.text = "Targets hit: " + GameManager.capturedTargetsHit.ToString("0");

            //Props stuff:
            badGameEndingPropsGroup.SetActive(true);
            goodGameEndingPropsGroup.SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
