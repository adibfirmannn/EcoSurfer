using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bool success = other.GetComponent<PlayerInventory>().AddItem(item);
            if (success)
            {
                Destroy(gameObject);
            }
        }
    }
}
