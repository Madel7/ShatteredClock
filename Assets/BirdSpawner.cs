using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject[] birdTypes;

    public Transform[] spawnPoints;

    public float spawnDelay = 5f;
    public float destroyAfter = 15f;

    public float birdSpeed = 5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnBird), 1f, spawnDelay);
    }

    void SpawnBird()
    {
        GameObject randomBird =
            birdTypes[Random.Range(0, birdTypes.Length)];

        Transform randomPoint =
            spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject bird =
            Instantiate(
                randomBird,
                randomPoint.position,
                randomPoint.rotation
            );

        Rigidbody rb = bird.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = bird.transform.forward * birdSpeed;
        }

        Destroy(bird, destroyAfter);
    }
}