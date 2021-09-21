// 4° MainLevel Script: Is in charge of the management of the enemy behaviour. 

using UnityEngine;

public class Target_Logic : MonoBehaviour
{
    #region Variables to use:
    [Header("Target Attributes:")]
    public float health = 10f;
    public GameObject destructionEffect;
    public AudioSource audioSource;
    public AudioClip scaryLaugh;
    #endregion

    #region Frame Methods: 
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayEnemySound();
    }
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
        Instantiate(destructionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        audioSource.Stop();
    }
    private void PlayEnemySound()
    {
        audioSource.PlayOneShot(scaryLaugh);
    }
    #endregion
}
