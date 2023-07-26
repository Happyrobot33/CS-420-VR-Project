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
        SceneManager.LoadScene("SpawnScene");
    }

    public void QuitGame()
    {
        gameHasEnded = true;
        Application.Quit();  //Ignored in Unity Editor but will shut down the running application
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
