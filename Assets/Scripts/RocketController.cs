using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [SerializeField] Rigidbody rb = null;
    [SerializeField] ParticleSystem ps = null;
    [SerializeField] GameObject art = null;
    
    public float velocity = 5f;
    private bool hasExploded = false;

    public void Start()
    {
        rb.velocity = transform.forward * velocity;
        Destroy(gameObject, 8f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasExploded)
        {
            hasExploded = true;
            //if collides with player
            if (other.gameObject.tag == "Player")
                other.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);

            //destroy rocket but wait for particle system
            rb.Sleep();
            art.SetActive(false);
            Destroy(gameObject, ps.main.startLifetime.constant);
        }

    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (!hasExploded)
        {
            hasExploded = true;
            //if collides with player
            if (collision.gameObject.tag == "Player")
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);

            //destroy rocket but wait for particle system
            rb.Sleep();
            art.SetActive(false);
            Destroy(gameObject, ps.main.startLifetime.constant);
        }
    }
    */
}
