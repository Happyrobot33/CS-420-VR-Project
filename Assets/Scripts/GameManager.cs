using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static bool ranOutOfTime;
    [HideInInspector]
    public static bool didTheyWin;
    [HideInInspector]
    public static float capturedTimeSeconds;
    [HideInInspector]
    public static int capturedTargetsHit;

    private bool gameHasEnded = false;

    public void NewGame()
    {
        gameHasEnded = false;
        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
        gameHasEnded = true;

        // Use pre-processor if directives to determine HOW to quit depending on if is a build or if is Editor runtime:

        //Application.Quit();  //Ignored in Unity Editor but will shut down the running application
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();  //Ignored in Unity Editor but will shut down the running application
#endif
    }

    public void BackToStart()
    {
        gameHasEnded=true;
        didTheyWin = false;
        SceneManager.LoadScene("StartMenu");
    }

    public void TimeExpired()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            ranOutOfTime = true; // Access this from GameOverScene to initiate different .SetActive()'s for unique UI
            didTheyWin = false;
            capturedTimeSeconds = 0;
            capturedTargetsHit = WristUIController.GetCount();
            Debug.Log("GAME OVER");
            SceneManager.LoadScene("GameOver");
        }
    }

    public void WonTheGame()
    {
        gameHasEnded = true;
        ranOutOfTime = false; // Access this from GameOverScene to initiate different .SetActive()'s for unique UI
        didTheyWin = true;
        capturedTimeSeconds = WristUIController.GetRemainingTime();
        capturedTargetsHit= WristUIController.GetCount();
        Debug.Log("WON THE GAME");
        SceneManager.LoadScene("GameOver");
    }

    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
