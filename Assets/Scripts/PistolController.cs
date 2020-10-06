using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolController : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleParticle;
    [SerializeField] AudioSource shootSound;

    private void Update()
    {
        //Shoot on left click (and if not paused)
        if (Input.GetMouseButtonDown(0) && Time.timeScale > 0f)
        {
            muzzleParticle.Play();
            shootSound.Play();
            Fire();
        }
    }

    private void Fire()
    {

    }
}
