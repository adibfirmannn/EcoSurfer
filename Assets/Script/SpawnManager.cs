using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public Transform spawnParent;         // assign GameObject kosong di scene

    [Header("Prefabs")]
    public GameObject[] trashPrefabs;
    public GameObject[] obstaclePrefabs;

    [Header("Spawn Settings")]
    public float spawnDistance = 30f;
    public float spawnInterval = 10f;
    public float spawnY = 1f;             // Naikkan spawn di atas ground
    public float laneOffset = 3f;

    private float nextSpawnZ;

    void Start()
    {
        nextSpawnZ = player.transform.position.z + spawnDistance;
    }

    void Update()
    {
        if (player.transform.position.z + spawnDistance > nextSpawnZ)
        {
            SpawnRow();
            nextSpawnZ += spawnInterval;
        }
    }

    void SpawnRow()
    {
        if ((trashPrefabs.Length == 0) && (obstaclePrefabs.Length == 0))
        {
            Debug.LogError("SpawnerManager: Belum assign prefab!");
            return;
        }

        List<int> usedLanes = new List<int>();
        int count = Random.Range(1, 3);

        for (int i = 0; i < count; i++)
        {
            int lane = Random.Range(0, 3);
            while (usedLanes.Contains(lane))
                lane = Random.Range(0, 3);
            usedLanes.Add(lane);

            Vector3 pos = new Vector3(
                (lane - 1) * laneOffset,
                spawnY,
                nextSpawnZ
            );

            GameObject prefab = (Random.value < 0.7f && trashPrefabs.Length > 0)
                ? trashPrefabs[Random.Range(0, trashPrefabs.Length)]
                : obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

            Debug.Log($"Spawning {prefab.name} at {pos}");
            Instantiate(prefab, pos, Quaternion.identity, spawnParent);
        }
    }
}
