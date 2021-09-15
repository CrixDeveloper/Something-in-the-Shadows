// 4° MainLevel Script: Is in charge of the management of the enemy behaviour. 

using UnityEngine;
using UnityEngine.AI;

public class Target_Logic : MonoBehaviour
{
    #region Variables to use:
    [Header("Target Attributes:")]

    public float health = 50f;
    public float moveSpeed = 2f;
    public static int damage = 25;

    public GameObject destructionEffect;
    public Transform playerReference;
    private Rigidbody rb;
    private Vector3 movement;
    
    [Header("Audio References:")]

    protected AudioSource audioSource;
    public AudioClip targetSound;

    #endregion

    #region Frames Methods: 
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        Vector3 direction = playerReference.position - transform.position;
        rb.rotation = Quaternion.identity;
        direction.Normalize();
        movement = direction;
    }
    private void FixedUpdate()
    {
        ChasePlayer(movement);
    }
    #endregion

    #region Methods to use:
    public void takeDamage(float amount)
    {
        audioSource.PlayOneShot(targetSound);

        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }
    public static void MakeDamage()
    {
        PlayerLife_Logic.playerLife += damage;
    }
    private void Die()
    {
        Instantiate(destructionEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }
    private void ChasePlayer(Vector3 direction)
    {
        rb.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));
    }
    #endregion
}
