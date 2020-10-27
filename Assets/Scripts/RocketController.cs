using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : Freezable
{
    [SerializeField] Rigidbody rb = null;
    [SerializeField] ParticleSystem rocketTrail = null;
    [SerializeField] GameObject art = null;
    [SerializeField] ParticleSystem explosion = null;
    [SerializeField] AudioSource audioSource = null;
    
    public float velocity = 5f;
    private bool hasExploded = false;
    private float timeTillDeath = 10f;
    private float deathTimer = 0f;

    public void Start()
    {
        rb.velocity = transform.forward * velocity;
    }

    private void Update()
    {
        UpdateFreeze();
        if (!isFrozen)
        {
            deathTimer += Time.deltaTime;
            if (deathTimer >= timeTillDeath)
                Destroy(gameObject);
        }

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!isFrozen && !hasExploded)
        {
            hasExploded = true;
            //if collides with player
            if (other.gameObject.tag == "Player")
                other.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);

            //explode effect
            explosion.gameObject.SetActive(true);
            audioSource.Play();

            //destroy rocket but wait for particle system
            rb.Sleep();
            art.SetActive(false);
            //Time until destroy depends on which particle system takes longer
            Destroy(gameObject, Mathf.Max(rocketTrail.main.startLifetime.constant, explosion.main.startLifetime.constant));
        }

    }

    //Freezing
    public override void Freeze()
    {
        isFrozen = true;
        frozenObject.SetActive(true);
        rb.velocity = new Vector3();
    }

    public override void Unfreeze()
    {
        isFrozen = false;
        frozenObject.SetActive(false);
        rb.velocity = transform.forward * velocity;
        deathTimer = 0f;
    }
}
