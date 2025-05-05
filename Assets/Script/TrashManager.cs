using UnityEngine;

public class TrashManager : MonoBehaviour
{
    public GameObject[] trashPrefabs;
    public Transform[] spawnPoints;

    private int spawnedTrash = 0;
    private int maxTrash = 10;

    public void SpawnTrash()
    {
        if (spawnedTrash >= maxTrash) return;

        int index = Random.Range(0, trashPrefabs.Length);
        int point = Random.Range(0, spawnPoints.Length);
        Instantiate(trashPrefabs[index], spawnPoints[point].position + Vector3.up, Quaternion.identity);

        spawnedTrash++;
    }
}
