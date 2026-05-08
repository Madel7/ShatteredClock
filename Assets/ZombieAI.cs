using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;

    public Transform player;
    public float attackRange = 2f;
    public int damage = 10;
    public float attackCooldown = 1.5f;

    float lastAttackTime;
    bool isDead = false;

    public AudioSource audioSource;
    public AudioClip deathSound;
    public AudioClip attackSound;
    public AudioClip walkSound;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (isDead) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);

            anim.SetBool("isRunning", true);
            if (!audioSource.isPlaying)
            {
                audioSource.clip = walkSound;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            agent.isStopped = true;
            anim.SetBool("isRunning", false);
            audioSource.Stop();
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        anim.SetTrigger("attack");

        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            ph.TakeDamage(damage);
        }
        audioSource.PlayOneShot(attackSound);
        lastAttackTime = Time.time;
    }

    public void Die()
    {
        isDead = true;

        agent.isStopped = true;
        agent.enabled = false;

        anim.SetBool("isRunning", false);
        anim.SetTrigger("die");
        audioSource.PlayOneShot(deathSound);
        this.enabled = false;
    }
}