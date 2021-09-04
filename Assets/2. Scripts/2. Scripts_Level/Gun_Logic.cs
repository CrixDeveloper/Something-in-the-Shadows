// 3° MainLevel Script: Is in charge of the management and behaviour of the guns of the player. 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Logic : MonoBehaviour
{
    #region Variables to use:
    [Header("Weapon Attributes:")]
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    [Header("FPS-Camera:")]
    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;

    private float nextTimeToFire = 0f;

    [Header("Audio References:")]
    protected AudioSource audioSource;
    public AudioClip fireSound;
    #endregion

    #region Frames Methods:
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }
    private void Shoot()
    {
        audioSource.PlayOneShot(fireSound);
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target_Logic target = hit.transform.GetComponent<Target_Logic>();
            if (target != null)
            {
                target.takeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
    }
    #endregion
}
