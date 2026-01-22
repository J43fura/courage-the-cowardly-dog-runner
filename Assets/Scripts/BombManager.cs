using UnityEngine;

public class BombManager : MonoBehaviour
{
    [Header("References")]
    public GameObject bombPrefab;
    public Transform player;
    public GameObject explosionEffect;

    [Header("Spawn Settings")]
    public float minZDistance = 10f;
    public float maxZDistance = 40f;
    public float laneDistance = 3f;
    public int lanes = 3;
    public float minSpawnGap = 25f;


    private float lastSpawnZ = 0f;

    void Update()
    {
        if (!GameUIManager.instance.AreBombsEnabled()) return;

        if (player == null) return;

        if (player.position.z + maxZDistance > lastSpawnZ + minSpawnGap)
        {
            SpawnBomb();
        }
    }

    void SpawnBomb()
    {
        int lane = Random.Range(0, lanes);
        float xPos = (lane - 1) * laneDistance;
        float zPos = lastSpawnZ + Random.Range(minZDistance, maxZDistance);

        Vector3 spawnPos = new Vector3(xPos, 12f, zPos);

        GameObject bomb = Instantiate(bombPrefab, spawnPos, Quaternion.identity);

        // Assign explosion effect to bomb if it has Bomb script
        Bomb bombScript = bomb.GetComponent<Bomb>();
        if (bombScript != null && explosionEffect != null)
        {
            bombScript.explosionEffect = explosionEffect;
        }

        lastSpawnZ = zPos;
    }
}
