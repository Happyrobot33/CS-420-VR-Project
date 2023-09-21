using UnityEngine;

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
    public float targetSpawnTime = 1;

    [Tooltip("Max ammount of targets that can be active at once")]
    public int maxTargets = 10;
    [Tooltip("Minimum target spawn distance from the player")]
    public float minSpawnDistance = 30;
    [Tooltip("Maximum target spawn distance from the player")]
    public float maxSpawnDistance = 55;
    [Tooltip("Max height of the target spawn")]
    public float maxHeight = 18;

    [Tooltip("Time in seconds for the level")]
    public float timeLimit = 60;

    /// <summary>
    /// Applies the level settings to the current scene
    /// </summary>
    /// <param name="levelSettings"></param>
    public static void ApplyToCurrentScene(LevelSettingsSO levelSettings)
    {
        Debug.Log("Level Name: " + levelSettings.name);

        //get the target spawner
        TargetSpawner targetSpawner = FindObjectOfType<TargetSpawner>();
        //set the target spawner's spawn interval
        targetSpawner.spawnInterval = levelSettings.targetSpawnTime;
        //set the target spawner's max active targets
        targetSpawner.maxActiveTargets = levelSettings.maxTargets;
        //set the target spawner's inner radius
        targetSpawner.innerRadius = levelSettings.minSpawnDistance;
        //set the target spawner's outer radius
        targetSpawner.outerRadius = levelSettings.maxSpawnDistance;
        //set the target spawner's height
        targetSpawner.height = levelSettings.maxHeight;

        //get the WristUIController
        WristUIController wristUIController = FindObjectOfType<WristUIController>();

        //set the wrist UI controller's time limit
        WristUIController.SetRemainingTime(levelSettings.timeLimit);
        //set the wrist UI controller's required targets
        wristUIController.winThreshold = levelSettings.requiredTargets;
    }
}
