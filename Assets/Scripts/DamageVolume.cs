using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVolume : MonoBehaviour
{
    public int damageDealt = 1;
    
    private void OnTriggerEnter(Collider other)
    {
        //if collides with player, deal damage
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damageDealt);
        }
    }
}
