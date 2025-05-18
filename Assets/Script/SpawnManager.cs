using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] trashPrefabs;
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

    void Start()
    {
        StartCoroutine(StartSpawningAfterDelay(5f)); // tunggu 20 detik
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

        if (trashSpawned < maxTrash)
        {
            int lane = Random.Range(-1, 2);
            Vector3 trashPos = new Vector3(lane * 2f, 2f, zPos + 2f);
            GameObject trash = Instantiate(trashPrefabs[Random.Range(0, trashPrefabs.Length)], trashPos, Quaternion.identity);
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
}
