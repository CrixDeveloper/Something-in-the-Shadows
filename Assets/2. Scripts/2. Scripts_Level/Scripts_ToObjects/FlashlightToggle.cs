// 1° MainLevel Script: Is in charge of turning on the flashlight for the player can walk in the dark room. 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FlashlightToggle : MonoBehaviour
{
    #region Variables to use: 
    public GameObject lightGO; //light gameObject to work with
    private bool isOn = false; //is flashlight on or off?
    #endregion

    #region Frame Methods to use: 
    // Use this for initialization
    void Start()
    {
        //set default off
        lightGO.SetActive(isOn);
    }

    // Update is called once per frame
    void Update()
    {
        //toggle flashlight on key down
        if (Input.GetKeyDown(KeyCode.X))
        {
            //toggle light
            isOn = !isOn;
            //turn light on
            if (isOn)
            {
                lightGO.SetActive(true);
            }
            //turn light off
            else
            {
                lightGO.SetActive(false);

            }
        }
    }
    #endregion
}
