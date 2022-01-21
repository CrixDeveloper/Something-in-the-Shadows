using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

namespace Interlude
{
    public class Pauser : MonoBehaviour
    {
        public void PauseGame()
        {
            //Pause script here

            //FindObjectOfType<StarterAssets.StarterAssetsInputs>().cursorInputForLook = false;
            //FindObjectOfType<StarterAssets.StarterAssetsInputs>().cursorLocked = false;
        }

        public void UnpauseGame()
        {
            //Unpause (resume) script here

            //FindObjectOfType<StarterAssets.StarterAssetsInputs>().cursorInputForLook = true;
            //FindObjectOfType<StarterAssets.StarterAssetsInputs>().cursorLocked = true;
        }
    }
}

