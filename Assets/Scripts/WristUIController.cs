using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WristUIController : MonoBehaviour
{
    public TextMeshProUGUI targetCountTextUI;
    public TextMeshProUGUI timerTextUI;

    public float initialTimeSeconds = 0;
    [HideInInspector]
    public bool isTimerRunning = false;
    public int winThreshold = 1;

    private static float timeLeft;
    private static int count;
    private int rawConvertedHours;
    private int rawConvertedMinutes;
    private int rawConvertedSeconds;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        timeLeft = initialTimeSeconds;

        //Store time remaining conversions from seconds into respective variables:
        rawConvertedHours = TimeSpan.FromSeconds(timeLeft).Hours;
        rawConvertedMinutes = TimeSpan.FromSeconds(timeLeft).Minutes;
        rawConvertedSeconds = TimeSpan.FromSeconds(timeLeft).Seconds;

        //isTimerRunning = false;
        isTimerRunning = true; //For testing purposes, timer is started at the beginning of the game

        SetCountText();

        SetTimerText();

    }

    // Update is called once per frame
    void Update()
    {
        //SetTimerText();

        // If the amount of targets recorded to be knocked = win condition:
        if (count == winThreshold)
        {
            // Then end the game prematurely, showing the amount of time left and the total number of targets hit:
            isTimerRunning = false;
            FindObjectOfType<GameManager>().WonTheGame();
        }

        if (isTimerRunning)
        {
            if (timeLeft > 0.00f)
            {
                // Keep counting it down!
                timeLeft -= Time.deltaTime;

                //Store time remaining conversions from seconds into respective variables:
                rawConvertedHours = TimeSpan.FromSeconds(timeLeft).Hours;
                rawConvertedMinutes = TimeSpan.FromSeconds(timeLeft).Minutes;
                rawConvertedSeconds = TimeSpan.FromSeconds(timeLeft).Seconds;
            }
            else
            {
                timeLeft = 0;
                isTimerRunning = false;
                // Make sure to have GameManager in your scene when using this function,
                // along with destination scene in build settings:
                FindObjectOfType<GameManager>().TimeExpired(); // Call GameManager's TimeExpired() function
            }
        }

        SetTimerText();
    }

    void SetCountText()
    {
        targetCountTextUI.text = "Targets hit: \n" + count.ToString();
    }

    void SetTimerText()
    {
        // Will format timer string like so: "00:00:00", using converted time ints
        string timerString = string.Format("{0:00}:{1:00}:{2:00}", rawConvertedHours, rawConvertedMinutes, rawConvertedSeconds);

        //timerTextUI.text = "Time left: " + timeLeft.ToString("0.0");
        timerTextUI.text = "Time left: \n" + timerString;
    }

    // Use SetCount() and IncrementCountBy() whenever a target is hit
    public static void SetCount(int newCount)
    {
        count = newCount;
    }

    public static void IncrementCountBy(int addedToCount)
    {
        count += addedToCount;
    }

    public static int GetCount() { return count; }

    public static float GetRemainingTime() { return timeLeft; }
}
