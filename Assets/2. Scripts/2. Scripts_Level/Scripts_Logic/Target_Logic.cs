// 4° MainLevel Script: Is in charge of the management of the enemy behaviour. 

using UnityEngine;

public class Target_Logic : MonoBehaviour
{
    #region Variables to use:
    [Header("Target Attributes:")]

    public float health = 10f;
    public GameObject destructionEffect;
    #endregion

    #region Methods to use:
    public void takeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }
    private void Die()
    {
        FindObjectOfType<WaveSpawner>().audioSource.Pause();
        Instantiate(destructionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    #endregion
}
