using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public GameObject tilePrefab;

    [Header("Tile Settings")]
    public float tileWidth = 4f;
    public float minTileLength = 10f;
    public float maxTileLength = 10f;
    public int tilesOnScreen = 10;

    private float spawnZ = 0f;
    private List<GameObject> activeTiles = new List<GameObject>();

    // Lane positions: Left (-1), Middle (0), Right (1)
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
            lane * tileWidth,   // X (lane)
            10f,                // Y fixed at 10
            spawnZ              // Z forward
        );

        GameObject tile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);

        // Optional: scale length if needed
        // tile.transform.localScale = new Vector3(1, 1, tileLength / 10f);

        activeTiles.Add(tile);
        spawnZ += tileLength;
    }

    void DeleteOldTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
