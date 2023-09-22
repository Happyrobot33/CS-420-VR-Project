using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WristUIController : MonoBehaviour
{
    // To be used with making the Wrist Menu visible/invisible upon facing the player's vision:
    public GameObject WristMenuUIObject;
    public GameObject SmallWristChestObject;
    public GameObject vrCameraCollider;

    // To be used with keeping track of game's runtime and progress:
    public TextMeshProUGUI targetCountTextUI;
    public TextMeshProUGUI timerTextUI;
    public AudioSource TargetScoredSound;

    public float initialTimeSeconds = 0;
    [HideInInspector]
    public bool isTimerRunning = false;
    public int winThreshold = 1;

    private static float timeLeft;
    private float wristUIDebounceTime;
    private int prevCount;
    private static int count;
    private int rawConvertedHours;
    private int rawConvertedMinutes;
    private int rawConvertedSeconds;

    bool checkToBecomeVisible()
    {
        RaycastHit whatWasHit;
        
        // Get the Z-Vectors from each object on the wrist:
        Vector3 localWristForward = (WristMenuUIObject.transform.forward);
        Vector3 localSmallChestForward = (SmallWristChestObject.transform.forward);
        
        Ray wristMenuRay = new Ray(WristMenuUIObject.transform.position, localWristForward);
        Ray smallWristChestRay = new Ray(WristMenuUIObject.transform.position, localSmallChestForward);

        Debug.DrawRay(WristMenuUIObject.transform.position, localWristForward * 10, Color.blue); // For testing

        if((Physics.Raycast(wristMenuRay, out whatWasHit)) || (Physics.Raycast(smallWristChestRay, out whatWasHit)))
        {
            // If the wrist UI ray or chest ray hits the camera collider, then make the wrist UI visible:
            if (whatWasHit.collider.name == vrCameraCollider.name)
            {
                WristMenuUIObject.SetActive(true);
                return true;
            }
            // Else, let the wrist UI's visibility expire:
            else
            {
                // Give it some extra visibility time if the debounce still has time left:
                if (wristUIDebounceTime > 0)
                {
                    WristMenuUIObject.SetActive(true);
                }
                else
                {
                    WristMenuUIObject.SetActive(false);
                }
                
                return false;
            }
        }
        else { return false; }
    }

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        timeLeft = initialTimeSeconds;
        wristUIDebounceTime = 2.0f;

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

        bool shouldWristUIRefresh = checkToBecomeVisible();
        if (shouldWristUIRefresh)
        {
            // Reset timer back to 2 seconds:
            wristUIDebounceTime = 2.0f;
        }
        else
        {
            if (wristUIDebounceTime > 0)
            {
                wristUIDebounceTime -= Time.deltaTime;
            }
            // Else, do nothing; it's at 0 already
        }

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
        //check if the count has changed
        if (prevCount != count)
        {
            //play the target scored sound
            TargetScoredSound.Play();
            //update the count text
            SetCountText();
            //update the previous count
            prevCount = count;
        }
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
    /// <summary>
    /// Sets the count to a new value
    /// </summary>
    /// <param name="newCount"></param>
    public static void SetCount(int newCount)
    {
        count = newCount;
    }

    /// <summary>
    /// Increments the count by a value
    /// </summary>
    /// <param name="addedToCount"></param>
    public static void IncrementCountBy(int addedToCount)
    {
        count += addedToCount;
    }

    /// <summary>
    /// Returns the current count
    /// </summary>
    /// <returns></returns>
    public static int GetCount() { return count; }

    /// <summary>
    /// Returns the remaining time
    /// </summary>
    /// <returns></returns>
    public static float GetRemainingTime() { return timeLeft; }

    /// <summary>
    /// Sets the remaining time
    /// </summary>
    /// <param name="newTime"></param>
    public static void SetRemainingTime(float newTime) { timeLeft = newTime; }

}
