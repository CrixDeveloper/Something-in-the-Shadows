// 7° MainLevel Script: Is in charge of instantiate the enemies in the dark room. 

using System.Collections;
using UnityEngine;

public class Instantiator_Logic : MonoBehaviour
{
    #region Variables to use: 
    public GameObject electroEscapePanel;
    public int xPos;
    public int zPos;
    public int electroPanelCount;
    #endregion

    #region Methods to use:
    private void Start()
    {
        
    }
    private void Update()
    {
        if (CountDown_Logic.secondsLeft == 90)
        {
            ElectroPanelDrop();
        }
    }
    private void ElectroPanelDrop()
    {
        if (electroPanelCount == 1)
        {
            xPos = Random.Range(-15, 15);
            zPos = Random.Range(-15, -40);
            Instantiate(electroEscapePanel, new Vector3(xPos, 3, zPos), Quaternion.identity);
            electroPanelCount = 0;
        }
    }
    #endregion
}
