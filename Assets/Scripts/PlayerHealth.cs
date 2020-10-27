using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Level01Controller gameController;
    [SerializeField] Text healthText;
    [SerializeField] HealthBar healthBar;
    [SerializeField] AudioSource loseSound;

    public int maxHealth = 5;
    int health;

    private void Start()
    {
        health = maxHealth;
        UpdateHealth();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
        UpdateHealth();
    }

    public void Heal(int healing)
    {
        health += healing;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        UpdateHealth();
    }

    void UpdateHealth()
    {
        healthText.text = "HP: " + health + "/" + maxHealth;
        healthBar.SetHealth((float) health /  (float) maxHealth);
    }
    
    void Die()
    {
        loseSound.Play();
        gameController.GameOver();
    }
}
