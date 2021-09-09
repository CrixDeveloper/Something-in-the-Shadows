// 7° MainLevel Script: Is in charge of instantiate the enemies in the dark room. 

using System.Collections;
using UnityEngine;

public class Instantiator_Logic : MonoBehaviour
{
    #region Variables to use: 
    public GameObject electroPanel;
    public int xPos;
    public int zPos;
    public static bool panelSpawned = false;
    #endregion

    #region Frames Methods:
    private void Update()
    {
        if (CountDown_Logic.secondsLeft == 90)
        {
            SpawnElectroPanel();
        }
    }

    private void SpawnElectroPanel()
    {
        if (panelSpawned == false)
        {
            xPos = Random.Range(-19, 9);
            zPos = Random.Range(-20, -35);
            Instantiate(electroPanel, new Vector3(xPos, 2, zPos), Quaternion.identity);
            panelSpawned = true;
        }else
        {
            if (panelSpawned == true)
            {
                return;
            }
        }
    }
    #endregion
}
