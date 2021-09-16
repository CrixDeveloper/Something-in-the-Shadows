using UnityEngine;
using UnityEngine.SceneManagement;

public class Logic_GameOver : MonoBehaviour
{
    #region Method to use:
    private int delay = 10;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("RestartReference", delay);
    }
    private void RestartReference()
    {
        GameManager_Logic.RestartGame();
    }
    #endregion
}
