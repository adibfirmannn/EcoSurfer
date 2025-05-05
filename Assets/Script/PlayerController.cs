using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Item currentItem;

    public void PickUpItem(Item item)
    {
        currentItem = item;
        Debug.Log("Picked up: " + item.itemName);
    }

    public Item GetCurrentItem()
    {
        return currentItem;
    }

    public void ClearCurrentItem()
    {
        currentItem = null;
    }
}
