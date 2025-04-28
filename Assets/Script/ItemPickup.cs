using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemPickup : MonoBehaviour
{
    [Header("Data Item")]
    public ItemData itemData;

    private PlayerInventory playerInventory;

    private void Awake()
    {
        playerInventory = Object.FindFirstObjectByType<PlayerInventory>();
        if (playerInventory == null)
            Debug.LogError("ItemPickup: PlayerInventory tidak ditemukan di scene!");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Pastikan hanya player yang bisa mem-pickup
        if (other.CompareTag("Player"))
        {
            playerInventory.AddItem(itemData);
            Destroy(gameObject);
        }
    }
}
