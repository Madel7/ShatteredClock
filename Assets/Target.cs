using UnityEngine;

public class Target : MonoBehaviour
{
    public int health = 50;
    private bool isDead = false;

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        Debug.Log("Zombie Died");

        Animator anim = GetComponentInChildren<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("die");
        }

        Destroy(gameObject, 0.6f);
    }
}