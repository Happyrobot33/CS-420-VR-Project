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

    public void SetLevel(LevelSettingsSO levelSettings)
    {
        GameManager.levelSettings = levelSettings;
    }

    //coroutine for when the scene is loaded
    IEnumerator LoadLevel()
    {
        var asyncLoad = SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single); //load the scene
        //wait for the scene to load
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        //check to make sure the level settings are set
        if (levelSettings == null)
        {
            //if not, set them to the fallback
            levelSettings = fallbacklevelSettings;
        }

        //wait 1 second
        yield return new WaitForSeconds(1);

        //once loaded, apply the level settings
        LevelSettingsSO.ApplyToCurrentScene(levelSettings);
    }

    /*
    void Start()
    {
    }*/
}

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

//scriptable object for the level settings
[CreateAssetMenu(fileName = "LevelSettings", menuName = "LevelSettings", order = 1)]
public class LevelSettingsSO : ScriptableObject
{
    public string name = "UNDEF";
    public string description = "UNDEF";

    //1-5?
    [Range(0, 5)]
    [Tooltip("1-5 Difficulty Rating")]
    public int difficulty = 0;

    [Tooltip("Number of targets required to win")]
    public int requiredTargets = 1;

    [Tooltip("Time in seconds between each target spawn. Can be a decimal value.")]
    public int targetSpawnTime = 1;

    [Tooltip("Max ammount of targets that can be active at once")]
    public int maxTargets = 10;

    [Tooltip("Time in seconds for the level")]
    public float timeLimit = 60;

    public static void ApplyToCurrentScene(LevelSettingsSO levelSettings)
    {
        Debug.Log("Applying level settings to current scene");
        Debug.Log("Level Name: " + levelSettings.name);
        //get the target spawner
        TargetSpawner targetSpawner = FindObjectOfType<TargetSpawner>();
        //set the target spawner's spawn interval
        targetSpawner.spawnInterval = levelSettings.targetSpawnTime;
        //set the target spawner's max active targets
        targetSpawner.maxActiveTargets = levelSettings.maxTargets;
    }
}
