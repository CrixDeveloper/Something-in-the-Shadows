// 6° MainLevel Script: Is in charge of the whole game management in general.

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_Logic : MonoBehaviour
{
    #region Variables to use:
    private bool gameHasEnded = false;
    public float restartDelay = 1f;
    #endregion

    #region Methods to use:
    public void GameOver()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            SceneManager.LoadScene("GameOver");
            Invoke("RestartGame", restartDelay);
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion
}

