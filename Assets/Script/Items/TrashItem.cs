using UnityEngine;

public class TrashItem : MonoBehaviour
{
    [Header("Item Settings")]
    public ItemData itemData;

    [Header("Pickup Settings")]
    public float pickupRadius = 1f;
    public LayerMask playerLayer = 1 << 8; // Layer 8 untuk Player

    private SphereCollider pickupCollider;
    private bool isPickedUp = false;

    void Start()
    {
        SetupCollider();
        SetupVisuals();
    }

    void SetupCollider()
    {
        // Buat collider untuk pickup
        pickupCollider = gameObject.AddComponent<SphereCollider>();
        pickupCollider.isTrigger = true;
        pickupCollider.radius = pickupRadius;
    }

    void SetupVisuals()
    {
        if (itemData != null && itemData.itemSprite != null)
        {
            // Setup sprite renderer
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
                spriteRenderer = gameObject.AddComponent<SpriteRenderer>();

            spriteRenderer.sprite = itemData.itemSprite;
            spriteRenderer.color = itemData.itemColor;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Cek apakah yang menyentuh adalah player
        if (isPickedUp) return;

        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            PickupItem();
        }
    }

    void PickupItem()
    {
        if (isPickedUp) return;

        isPickedUp = true;

        // Coba tambahkan ke inventory
        bool success = InventoryManager.Instance.AddItem(itemData);

        if (success)
        {
            // Play pickup effect
            PlayPickupEffect();

            // Destroy item
            Destroy(gameObject);
        }
        else
        {
            // Inventory penuh
            isPickedUp = false;
            Debug.Log("Inventory penuh!");
        }
    }

    void PlayPickupEffect()
    {
        // Spawn pickup effect jika ada
        if (itemData.pickupEffect != null)
        {
            GameObject effect = Instantiate(itemData.pickupEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }

        // Play sound effect (opsional)
        // AudioManager.Instance.PlayPickupSound(itemData.itemType);
    }

    void OnDrawGizmosSelected()
    {
        // Visualisasi pickup radius di scene view
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}