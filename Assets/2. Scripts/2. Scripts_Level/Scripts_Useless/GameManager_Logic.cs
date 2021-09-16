// 6° MainLevel Script: Is in charge of the whole game management in general.

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_Logic : MonoBehaviour
{
    #region Variables to use:
    private bool gameHasEnded = false;
    public float restartDelay = 10f;
    #endregion

    #region Methods to use:
    public void GameOver()
    {
        if (gameHasEnded == false && CountDown_Logic.secondsLeft == 0)
        {
            gameHasEnded = true;
            SceneManager.LoadScene("GameOver");
            Invoke("RestartGame", restartDelay);
        }
    }
    public static void RestartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
    #endregion
}

