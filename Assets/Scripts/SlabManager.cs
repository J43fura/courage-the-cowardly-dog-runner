using UnityEngine;

public class SlabManager : MonoBehaviour
{
    [Header("References")]
    public GameObject slabPrefab;
    public Transform player;

    [Header("Spawn Settings")]
    public float minZDistance = 10f;
    public float maxZDistance = 40f;
    public float laneDistance = 3f;
    public int lanes = 3;
    public float minSpawnGap = 25f;

    private float lastSpawnZ = 0f;

    void Update()
    {
        if (player == null) return;

        if (player.position.z + maxZDistance > lastSpawnZ + minSpawnGap)
        {
            SpawnSlab();
        }
    }

    void SpawnSlab()
    {
        int lane = Random.Range(0, lanes);
        float xPos = (lane - 1) * laneDistance;
        float zPos = lastSpawnZ + Random.Range(minZDistance, maxZDistance);

        Vector3 spawnPos = new Vector3(xPos, 13f, zPos);

        GameObject slab = Instantiate(slabPrefab, spawnPos, Quaternion.Euler(0f, 180f, 0f));

        lastSpawnZ = zPos;
    }
}
