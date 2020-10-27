using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] Level01Controller levelController;
    [SerializeField] BoxCollider boxCollider;
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] GameObject art;
    [SerializeField] AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        //if collides with enemy and is small enough
        if (other.gameObject.CompareTag("Player"))
        {
            //give score
            levelController.IncreaseScore(1);

            //feedback
            audioSource.Play();
            particleSystem.Play();

            //delay destroy
            art.SetActive(false);
            boxCollider.enabled = false;
            Destroy(gameObject, 2f);
        }
    }

    private void Update()
    {
        //apply constant rotation based on difference in time
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
