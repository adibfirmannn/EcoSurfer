using UnityEngine;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public int numberOfTiles = 5;
    public float tileLength = 100;
    public Transform playerTransform;

    private List<GameObject> activeTiles = new List<GameObject>();
    private float spawnZ = 0;
    private float safeZone = 45.0f;

    void Start()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        if (playerTransform.position.z - safeZone > (spawnZ - numberOfTiles * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    void SpawnTile()
    {
        Vector3 spawnPosition = new Vector3(
            tilePrefab.transform.position.x,  // ikut X prefab
            tilePrefab.transform.position.y,  // ikut Y prefab
            spawnZ                             // hanya maju di Z
        );

        GameObject go = Instantiate(tilePrefab, spawnPosition, tilePrefab.transform.rotation);
        activeTiles.Add(go);
        spawnZ += tileLength;
    }

    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
