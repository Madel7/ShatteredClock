using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public float health;

    public Slider healthBar;

    public float healDelay = 5f;
    public float healSpeed = 5f;

    float lastDamageTime;

    public GameObject deathPanel;

    bool isDead = false;

    void Start()
    {
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = health;

        deathPanel.SetActive(false);
    }

    void Update()
    {
        if (isDead) return;

        if (Time.time - lastDamageTime > healDelay)
        {
            Heal();
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        healthBar.value = health;

        lastDamageTime = Time.time;

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
        isDead = true;

        Debug.Log("You Died");

        deathPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Retry()
    {
        GameManager.instance.zombieKills = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        GameManager.instance.zombieKills = 0;
        SceneManager.LoadScene("MainMenu");
    }
}