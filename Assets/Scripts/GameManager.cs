using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
        SceneManager.LoadScene("StartMenu");
    }

    public void TimeExpired()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            SceneManager.LoadScene("GameOverScene");
        }
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
