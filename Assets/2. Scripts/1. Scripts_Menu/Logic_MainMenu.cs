// 1° MainMenu Script: Is in charge of the actions of the buttons. 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logic_MainMenu : MonoBehaviour
{
    #region Methods to use:
    public void PlayGame()
    {
        SceneManager.LoadScene("MainLevel");
    }
    public void QuitGame()
    {
        Debug.Log("Closing Game...");
        Application.Quit();
    }
    #endregion
}
