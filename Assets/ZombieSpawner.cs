using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform[] spawnPoints;

    public float spawnInterval = 3f;
    public int maxZombies = 20;

    int currentZombies = 0;

    void Start()
    {
        InvokeRepeating("SpawnZombie", 2f, spawnInterval);
    }

    void SpawnZombie()
    {
        if (currentZombies >= maxZombies) return;

        if (spawnPoints.Length == 0) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        NavMeshHit hit;

        if (NavMesh.SamplePosition(spawnPoint.position, out hit, 2f, NavMesh.AllAreas))
        {
            GameObject zombie = Instantiate(zombiePrefab, hit.position, Quaternion.identity);

            currentZombies++;

            ZombieAI ai = zombie.GetComponent<ZombieAI>();
            if (ai != null)
            {
                ai.player = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }
    }
}