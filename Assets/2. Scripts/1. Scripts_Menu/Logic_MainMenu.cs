// 1° MainMenu Script: Is in charge of the actions of the buttons. 

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Logic_MainMenu : MonoBehaviour
{
    #region Variables to use:
    public GameObject loadingScreen;
    public Slider slider;
    #endregion

    #region Methods to use:
    public void PlayGame(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }
    private IEnumerator LoadAsynchronously (string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            yield return null;
        }
    }
    public void QuitGame()
    {
        Debug.Log("Closing Game...");
        Application.Quit();
    }
    #endregion
}
