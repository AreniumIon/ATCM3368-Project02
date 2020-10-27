using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] Level01Controller levelController;

    private void OnTriggerEnter(Collider other)
    {
        //if collides with enemy and is small enough
        if (other.gameObject.CompareTag("Player"))
        {
            levelController.IncreaseScore(1);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //apply constant rotation based on difference in time
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
