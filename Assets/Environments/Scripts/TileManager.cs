using UnityEngine;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    [Header("Prefab Settings")]
    public GameObject tilePrefab;

    [Header("Spawn Settings")]
    public int numberOfTiles = 5;
    public float tileLength = 46f;
    public Transform playerTransform;

    [Header("Advanced Settings")]
    public float safeZone = 120f;
    public bool useRelativePosition = true;

    public GameObject newCityTilePrefab; // kota baru
    public int switchAfterTiles = 5;     // jumlah tile sebelum pindah kota
    private int tilesPassed = 0;         // counter tile yang dilewati


    private Vector3 prefabPosition;
    private Quaternion prefabRotation;
    private List<GameObject> activeTiles = new List<GameObject>();
    private float spawnZ = 0f;
    private bool initialized = false;

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        // Pastikan ada prefab
        if (tilePrefab == null)
        {
            Debug.LogError("ERROR: Prefab tile belum diassign di Inspector! Assign prefab kota ke TileManager.", this);
            return;
        }

        if (playerTransform == null)
        {
            Debug.LogError("ERROR: Player Transform belum diassign di Inspector! Assign transform player ke TileManager.", this);
            return;
        }

        // Simpan posisi dan rotasi asli prefab
        prefabPosition = tilePrefab.transform.position;
        prefabRotation = tilePrefab.transform.rotation;

        // Gunakan posisi Z dari prefab sebagai titik mulai jika menggunakan posisi relatif
        if (useRelativePosition)
        {
            spawnZ = prefabPosition.z;
        }
        else
        {
            spawnZ = 0f;
        }

        //Debug.Log($"TileManager initialized. Prefab position: {prefabPosition}, Starting spawn Z: {spawnZ}");

        // Bersihkan daftar jika ada
        foreach (GameObject tile in activeTiles)
        {
            if (tile != null)
                Destroy(tile);
        }
        activeTiles.Clear();

        // Spawn tile awal
        for (int i = 0; i < numberOfTiles; i++)
        {
            SpawnTile();
        }

        initialized = true;
    }

    void Update()
    {
        if (!initialized)
            return;

        if (tilePrefab == null || playerTransform == null)
            return;

        // Cek jika player sudah bergerak cukup jauh untuk spawn tile baru
        if (playerTransform.position.z - safeZone > (spawnZ - numberOfTiles * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    void SpawnTile()
    {
        // Validasi prefab
        if (tilePrefab == null)
        {
            //Debug.LogError("Tidak dapat spawn tile karena prefab tidak diassign!");
            return;
        }

        // Gunakan posisi asli prefab untuk X dan Y, tapi gunakan spawnZ untuk posisi Z
        Vector3 spawnPosition;

        if (useRelativePosition)
        {
            spawnPosition = new Vector3(
                prefabPosition.x,
                prefabPosition.y,
                spawnZ
            );
        }
        else
        {
            spawnPosition = new Vector3(0, 0, spawnZ);
        }

        // Instantiate dengan posisi dan rotasi asli (atau default)
        GameObject tile = Instantiate(tilePrefab, spawnPosition, useRelativePosition ? prefabRotation : Quaternion.identity);

        // Tambahkan ke daftar tile aktif
        activeTiles.Add(tile);

        // Debug info
        //Debug.Log($"Spawn tile pada posisi: {spawnPosition}, total tiles aktif: {activeTiles.Count}");

        // Update titik spawn untuk tile berikutnya
        spawnZ += tileLength;
    }

    void DeleteTile()
    {
        if (activeTiles.Count > numberOfTiles)
        {
            if (activeTiles[0] != null)
                Destroy(activeTiles[0]);

            activeTiles.RemoveAt(0);

            tilesPassed++;
            Debug.Log("Tiles passed: " + tilesPassed);

            if (tilesPassed >= switchAfterTiles && newCityTilePrefab != null)
            {
                if (tilePrefab != newCityTilePrefab)
                {
                    tilePrefab = newCityTilePrefab;
                    Debug.Log("Kota baru dimulai!");
                }
            }
        }
    }



    // Untuk debugging - panggil dari editor jika perlu
    //[ContextMenu("Reinitialize")]
    //public void Reinitialize()
    //{
    //    Debug.Log("Manual reinitialization requested");
    //    Initialize();
    //}

    [ContextMenu("Log Active Tiles")]
    //public void LogActiveTiles()
    //{
    //    Debug.Log($"=== Total {activeTiles.Count} tiles aktif ===");
    //    for (int i = 0; i < activeTiles.Count; i++)
    //    {
    //        if (activeTiles[i] != null)
    //            Debug.Log($"Tile {i}: Position Z = {activeTiles[i].transform.position.z}");
    //        else
    //            Debug.Log($"Tile {i}: NULL REFERENCE");
    //    }
    //}

    void OnValidate()
    {
        // Tambahkan validasi inspector untuk membantu pengaturan
        if (tileLength <= 0)
            tileLength = 100f;

        if (numberOfTiles < 2)
            numberOfTiles = 2;
    }
}