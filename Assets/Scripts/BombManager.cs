using UnityEngine;

public class BombManager : MonoBehaviour
{
    [Header("References")]
    public GameObject bombPrefab;
    public Transform player;

    [Header("Spawn Settings")]
    public float minZDistance = 10f; // Min distance ahead of player to spawn
    public float maxZDistance = 20f; // Max distance ahead of player to spawn
    public float laneDistance = 3f;   // Distance between lanes (same as tiles)
    public int lanes = 3;             // Number of lanes (0 = left, 1 = middle, 2 = right)

    public float spawnInterval = 2f;  // How often bombs spawn
    private float lastSpawnZ = 0f;

    void Update()
    {
        if (player == null) return;

        // Check if we should spawn a bomb ahead of the player
        if (player.position.z + maxZDistance > lastSpawnZ)
        {
            SpawnBomb();
        }
    }

    void SpawnBomb()
    {
        // Choose a random lane
        int lane = Random.Range(0, lanes);

        // Calculate spawn position
        float xPos = (lane - 1) * laneDistance; // -1 = left, 0 = middle, 1 = right
        float zPos = lastSpawnZ + Random.Range(minZDistance, maxZDistance);

        Vector3 spawnPos = new Vector3(xPos, 10.5f, zPos); // 0.5f height for bomb

        Instantiate(bombPrefab, spawnPos, Quaternion.identity);

        lastSpawnZ = zPos; // Update last spawn position
    }
}

