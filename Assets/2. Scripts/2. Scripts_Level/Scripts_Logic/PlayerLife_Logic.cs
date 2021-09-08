using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife_Logic : MonoBehaviour
{
    #region Variables to use: 

    [Header("Player Attributes:")]

    public GameObject enemyRef;
    public Text lifeText;
    public static int playerLife;

    #endregion

    #region Frames Methods:
    // Start is called before the first frame update
    void Start()
    {
        playerLife = 0;
        lifeText.GetComponent<Text>().text = "Scare Meter = " + playerLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerLife == 0)
        {
            lifeText.GetComponent<Text>().color = Color.white;
            lifeText.GetComponent<Text>().text = "Scare Meter = " + playerLife;
        }
        else
        {
            if (playerLife == 25)
            {
                lifeText.GetComponent<Text>().color = Color.green;
                lifeText.GetComponent<Text>().text = "Scare Meter = " + playerLife;
            }
            else
            {
                if (playerLife == 50)
                {
                    lifeText.GetComponent<Text>().color = Color.yellow;
                    lifeText.GetComponent<Text>().text = "Scare Meter = " + playerLife;
                }
                else
                {
                    if (playerLife == 75)
                    {
                        lifeText.GetComponent<Text>().color = Color.red;
                        lifeText.GetComponent<Text>().text = "Scare Meter = " + playerLife;
                    }
                }
            }
        }
       
        if (playerLife == 100)
        {
            Time.timeScale = 0;
            FindObjectOfType<GameManager_Logic>().GameOver();
        }
    }
    #endregion

    #region Methods to use: 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Target_Logic.MakeDamage();
            Destroy(enemyRef);
        }
    }
    #endregion
}
