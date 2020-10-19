using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolController : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleParticle;
    [SerializeField] AudioSource shootSound;

    //firing stats
    public float cooldown = 0.2f;
    private float cooldownTimer = 0f;

    private void Update()
    {
        //Update timers
        cooldownTimer += Time.deltaTime;

        //Shoot on left click (and if not paused)
        if (Input.GetMouseButtonDown(0) && cooldownTimer >= cooldown)
        {
            //feedback
            muzzleParticle.Play();
            shootSound.Play();

            //shoot gun
            Fire();

            //reset timer
            cooldownTimer = 0f;
        }

    }

    private void Fire()
    {

    }
}
