using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Logic : MonoBehaviour
{
    #region Variables to use:

    [Header ("Stage References:")]
    public GameObject flashLight;
    public GameObject pistol;

    [Header("FPS References:")]
    public GameObject fpsFlashLight;
    public GameObject fpsPistol;

    #endregion

    #region Collision Method:
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PickUpLight"))
        {
            fpsFlashLight.SetActive(true);
            Destroy(flashLight);
        }
        if (collision.gameObject.CompareTag("PickUpPistol"))
        {
            fpsPistol.SetActive(true);
            Destroy(pistol);
        }
    }
    #endregion
}
