// 7° MainLevel Script: Is in charge of instantiate the enemies in the dark room. 

using System.Collections;
using UnityEngine;

public class Instantiator_Logic : MonoBehaviour
{
    #region Variables to use: 
    public GameObject enemy;
    public int xPos;
    public int zPos;
    public int enemyCount;
    #endregion

    #region Methods to use:
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(EnemyDrop());
        
    }
    private void Update()
    {
        
    }

    private IEnumerator EnemyDrop()
    {
        while (enemyCount < 10)
        {
            xPos = Random.Range(-10, 10);
            zPos = Random.Range(-20, -35);
            Instantiate(enemy, new Vector3(xPos, 3, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;
        }
    }
    #endregion
}
