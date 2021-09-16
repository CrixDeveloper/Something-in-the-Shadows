using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTurnOff : MonoBehaviour
{
    #region Method:
    public GameObject flashLightOff;
     
    
    // Update is called once per frame
    void Update()
    {
        if (CountDown_Logic.secondsLeft == Random.Range(0, 200))
        {
            flashLightOff.SetActive(false);
        }
    }
    #endregion
}
