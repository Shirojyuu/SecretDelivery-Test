using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static bool gameActive;
    private static bool debug;

    void Start () {
       
        gameActive = true;
        debug = false;
	}

    void Update()
    {

    }

    public static bool IsGamePausable()
    {
            return true;
    }

    public static bool IsGameActive()
    {
        return gameActive;
    }

    public static void SetGameActive(bool gameActive)
    {
        GameManager.gameActive = gameActive;
    }

    public static bool IsDebugEnabled()
    {
        return debug;
    }

    public static void SetDebugMode(bool debug)
    {
        GameManager.debug = debug;
    }

    public static void ChangeScene(int level)
    {
        SceneManager.LoadScene(level);
    }

    public static void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public static void NextLevel()
    {
        if(SceneManager.GetSceneAt(SceneManager.GetActiveScene().buildIndex + 1) == null)
        {
            //Loop back around
            SceneManager.LoadScene(0);
        }
        //Move to next scene if available, otherwise
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Ends the application
    //Why the hell do we need this again?
    //Now that we made a controller driven game, it makes sense
    public static void ExitGame()
    {
        Application.Quit();
    }
}
