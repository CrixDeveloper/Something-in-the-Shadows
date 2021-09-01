﻿// 5° MainLevel Script: Is in charge of the management of the countdown game object. 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown_Logic : MonoBehaviour
{
    #region Variables to use: 
    public GameObject textDisplay;
    public int secondsLeft = 90;
    public bool takingAway = false;
    #endregion

    #region Frames Methods: 
    private void Start()
    {
        textDisplay.GetComponent<Text>().text = "Time Left: " + secondsLeft;
    }

    private void Update()
    {
        if (takingAway == false && secondsLeft > 0)
        {
            StartCoroutine(TimerTake());
        }
        else
        {
            if (secondsLeft == 0)
            {
                Time.timeScale = 0;
                FindObjectOfType<GameManager_Logic>().GameOver();
            }
        }
    }
    #endregion

    #region Methods to use:
    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        textDisplay.GetComponent<Text>().text = "Time Left: " + secondsLeft;
        takingAway = false;
    }
    #endregion
}
