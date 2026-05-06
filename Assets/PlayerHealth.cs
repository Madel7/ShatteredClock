using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public float health;

    public Slider healthBar;

    [Header("Auto Heal")]
    public float healDelay = 5f;      // ????? ??? ????? ??? ?????
    public float healSpeed = 5f;      // ???? ????? (????? ???? ???? ????)

    float lastDamageTime;

    void Start()
    {
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }

    void Update()
    {
        // ?? ??? ??? ????? ?? ??? ???? ? ???? heal
        if (Time.time - lastDamageTime > healDelay)
        {
            Heal();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.value = health;

        lastDamageTime = Time.time; // ???? ?????

        if (health <= 0)
        {
            Die();
        }
    }

    void Heal()
    {
        if (health < maxHealth)
        {
            health += healSpeed * Time.deltaTime;
            healthBar.value = health;
        }

    }

    void Die()
    {
        Debug.Log("You Died");
        Time.timeScale = 0f;
    }
}