using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [Header("Prefab Settings")]
    public GameObject[] obstaclePrefabs;
    public GameObject[] trashPrefabs;
    public GameObject[] portalPrefabs;

    [Header("Player Reference")]
    public Transform player;

    [Header("Spawn Settings")]
    public float spawnZ = 30f;          // Jarak spawn dari player
    public float spawnInterval = 15f;   // Jarak antar spawn
    private float lastSpawnZ;
    private bool gameStarted = false;
    public float portalSpawnDistance = 100f;  // jarak portal dari posisi sampah (bisa kamu atur)

    void Start()
    {
        StartCoroutine(StartSpawningAfterDelay(3f));
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

            Vector3 obstaclePos = new Vector3(lane * 2f, 1.3f, zPos);
            Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], obstaclePos, Quaternion.identity);
        }

        if (trashPrefabs.Length > 0)
        {
            int trashLane = Random.Range(-1, 2);
            Vector3 trashPos = new Vector3(trashLane * 2f, 2f, zPos + 2f);
            Instantiate(trashPrefabs[Random.Range(0, trashPrefabs.Length)], trashPos, Quaternion.identity);

            int portalLane;
            do
            {
                portalLane = Random.Range(-1, 2);
            } while (portalLane == trashLane);

            float offsetX = -0.8f;  // posisi X portal yang sudah pas
            float offsetY = -0.5f;  // posisi Y portal yang sudah pas

            Vector3 portalPos = new Vector3(portalLane * 2f + offsetX, 1.5f + offsetY, trashPos.z + portalSpawnDistance);

            Debug.Log("Portal spawned at: " + portalPos);

            GameObject portal = Instantiate(portalPrefabs[Random.Range(0, portalPrefabs.Length)], portalPos, portalPrefabs[0].transform.rotation);

            Rigidbody rb = portal.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.linearVelocity = Vector3.zero;
            }
        }
    }


}
