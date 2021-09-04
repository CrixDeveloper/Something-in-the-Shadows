// 4° MainLevel Script: Is in charge of the management of the enemy behaviour. 

using UnityEngine;

public class Target_Logic : MonoBehaviour
{
    #region Variables to use:
    [Header("Target Attributes:")]
    public float health = 50f;
    public GameObject destructionEffect;

    [Header("Audio References:")]
    protected AudioSource audioSource;
    public AudioClip targetSound;
    #endregion

    #region Methods to use: 
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void takeDamage(float amount)
    {
        audioSource.PlayOneShot(targetSound);

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
    }
    #endregion
}
