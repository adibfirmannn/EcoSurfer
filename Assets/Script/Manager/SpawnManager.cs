using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [Header("Obstacle & Trash Settings")]
    public GameObject[] obstaclePrefabs;
    public GameObject[] trashPrefabsLegacy; // Legacy trash untuk SpawnRow
    public GameObject[] portalPrefabs;

    public Transform player;
    public float spawnZ = 30f;
    public float spawnInterval = 15f;

    private int maxTrash = 10;
    private int trashSpawned = 0;
    private int trashSinceLastPortal = 0;
    private List<GameObject> activeObjects = new List<GameObject>();

    private float lastSpawnZ;
    private bool gameStarted = false;

    [Header("Trash Spawning - EcoSurfer")]
    public TrashItem[] trashPrefabs; // Prefab trash versi baru
    public float trashSpawnRate = 2f;
    public Transform[] trashSpawnPoints;

    void Start()
    {
        StartCoroutine(StartSpawningAfterDelay(5f)); // tunggu 5 detik (bukan 20)

        // Mulai pemanggilan berkala untuk spawn trash
        if (trashPrefabs.Length > 0 && trashSpawnPoints.Length > 0)
        {
            InvokeRepeating("SpawnRandomTrash", 2f, trashSpawnRate);
        }
    }

    IEnumerator StartSpawningAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        lastSpawnZ = player.position.z;
        gameStarted = true;
    }

    void Update()
    {
        if (!gameStarted) return;

        if (player.position.z + spawnZ > lastSpawnZ)
        {
            SpawnRow(lastSpawnZ + spawnInterval);
            lastSpawnZ += spawnInterval;
        }
    }

    void SpawnRow(float zPos)
    {
        int obstacleCount = Random.Range(1, 4);
        List<int> usedLanes = new List<int>();

        for (int i = 0; i < obstacleCount; i++)
        {
            int lane;
            do
            {
                lane = Random.Range(-1, 2);
            } while (usedLanes.Contains(lane));
            usedLanes.Add(lane);

            Vector3 pos = new Vector3(lane * 2f, 1.3f, zPos);
            GameObject obj = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], pos, Quaternion.identity);
            activeObjects.Add(obj);
        }

        if (trashSpawned < maxTrash && trashPrefabsLegacy.Length > 0)
        {
            int lane = Random.Range(-1, 2);
            Vector3 trashPos = new Vector3(lane * 2f, 2f, zPos + 2f);
            GameObject trash = Instantiate(trashPrefabsLegacy[Random.Range(0, trashPrefabsLegacy.Length)], trashPos, Quaternion.identity);
            trashSpawned++;
            trashSinceLastPortal++;
            activeObjects.Add(trash);
        }

        if (trashSinceLastPortal >= 6 && trashSpawned > 0)
        {
            int lane = Random.Range(-1, 2);
            Vector3 portalPos = new Vector3(lane * 2f, 3f, zPos + 5f);
            GameObject portal = Instantiate(portalPrefabs[Random.Range(0, portalPrefabs.Length)], portalPos, Quaternion.identity);
            activeObjects.Add(portal);
            trashSinceLastPortal = 0;
        }
    }

    void SpawnRandomTrash()
    {
        if (trashPrefabs.Length > 0 && trashSpawnPoints.Length > 0)
        {
            TrashItem randomTrash = trashPrefabs[Random.Range(0, trashPrefabs.Length)];
            Transform spawnPoint = trashSpawnPoints[Random.Range(0, trashSpawnPoints.Length)];
            Instantiate(randomTrash, spawnPoint.position, spawnPoint.rotation);
        }
    }
}