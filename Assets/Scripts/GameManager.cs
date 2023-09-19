using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static bool ranOutOfTime;
    public static bool didTheyWin;
    public static float capturedTimeSeconds;
    public static int capturedTargetsHit;

    public LevelSettingsSO fallbacklevelSettings;
    public static LevelSettingsSO levelSettings;

    private bool gameHasEnded = false;

    public void NewGame()
    {
        gameHasEnded = false;

        //start loading coroutine
        StartCoroutine(LoadLevel());
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
        capturedTargetsHit = 0;
        SceneManager.LoadScene("StartMenu");
    }

    public void TimeExpired()
    {
        if (gameHasEnded == false)
        {
            // Store and report post-data of this game:
            gameHasEnded = true;
            ranOutOfTime = true; // Access this from GameOverScene to initiate different .SetActive()'s for unique UI
            didTheyWin = false;
            capturedTimeSeconds = 0;
            capturedTargetsHit = WristUIController.GetCount();

            // Reset static counter's count back to 0 for next game after storing this game's data:
            WristUIController.SetCount(0);
            // Reset static timer's time back to initial set time for next game after storing this game's data:
            //WristUIController.SetRemainingTime(WristUIController.GetInitialTime());
            Debug.Log("GAME OVER");
            SceneManager.LoadScene("GameOver");
        }
    }

    public void WonTheGame()
    {
        // Store and report post-data of this game:
        gameHasEnded = true;
        ranOutOfTime = false; // Access this from GameOverScene to initiate different .SetActive()'s for unique UI
        didTheyWin = true;
        capturedTimeSeconds = WristUIController.GetRemainingTime();
        capturedTargetsHit= WristUIController.GetCount();

        // Reset static counter's count back to 0 for next game after storing this game's data:
        WristUIController.SetCount(0);
        // Reset static timer's time back to initial set time for next game after storing this game's data:
        //WristUIController.SetRemainingTime(WristUIController.GetInitialTime());
        Debug.Log("WON THE GAME");
        SceneManager.LoadScene("GameOver");
    }

    public void SetLevel(LevelSettingsSO levelSettings)
    {
        GameManager.levelSettings = levelSettings;
    }

    //coroutine for when the scene is loaded
    IEnumerator LoadLevel()
    {
        //avoid destroying the game manager when loading a new scene with level settings
        DontDestroyOnLoad(gameObject);

        var asyncLoad = SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single); //load the scene
        //wait for the scene to load
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        //check to make sure the level settings are set
        if (levelSettings == null)
        {
            //if fallback is false then just destroy
            if (fallbacklevelSettings == null)
            {
                Destroy(gameObject);
                yield break;
            }
            //if not, set them to the fallback
            levelSettings = fallbacklevelSettings;
        }

        //once loaded, apply the level settings
        LevelSettingsSO.ApplyToCurrentScene(levelSettings);
        //destroy self since the scene already has a game manager
        Destroy(gameObject);
    }

    /*
    void Start()
    {
    }*/
}

#if UNITY_EDITOR
//custom editor
[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameManager myScript = (GameManager)target;
        if (GUILayout.Button("New Game"))
        {
            myScript.NewGame();
        }
        if (GUILayout.Button("Quit Game"))
        {
            myScript.QuitGame();
        }
    }
}
#endif

