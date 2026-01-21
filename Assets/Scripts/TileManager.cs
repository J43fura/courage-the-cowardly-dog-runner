using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public GameObject tilePrefab;

    [Header("Tile Settings")]
    public float tileWidth = 3f;          // MATCH laneDistance
    public float minTileLength = 10f;
    public float maxTileLength = 10f;
    public int tilesOnScreen = 20;

    private float spawnZ = 0f;
    private List<GameObject> activeTiles = new List<GameObject>();

    // Lane positions: Left, Middle, Right (match PlayerController)
    private float[] lanes = { -1f, 0f, 1f };

    void Start()
    {
        for (int i = 0; i < tilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        if (player.position.z + 40f > spawnZ)
        {
            SpawnTile();
            DeleteOldTile();
        }
    }

    void SpawnTile()
    {
        float tileLength = Random.Range(minTileLength, maxTileLength);
        float lane = lanes[Random.Range(0, lanes.Length)];

        Vector3 spawnPosition = new Vector3(
            lane * tileWidth,   // now = -3, 0, +3
            10f,                // same Y as player ground
            spawnZ              // connected Z
        );

        GameObject tile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);

        activeTiles.Add(tile);
        spawnZ += tileLength;  // seamless connection
    }

    void DeleteOldTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
