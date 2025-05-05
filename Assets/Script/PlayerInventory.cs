using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Item[] items = new Item[6];
    public int selectedSlot = 0;

    // Drag & drop ToolbarManager in Inspector or auto-find in Awake()
    public ToolbarManager toolbar;

    private void Awake()
    {
        // If not assigned, try to find the ToolbarManager in the scene
        if (toolbar == null)
        {
            toolbar = Object.FindFirstObjectByType<ToolbarManager>();
            if (toolbar == null)
                Debug.LogError("PlayerInventory: ToolbarManager not found in scene!");
        }
    }

    public bool AddItem(Item newItem)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = newItem;

                // Make sure toolbar is not null before updating the UI
                if (toolbar != null)
                    toolbar.UpdateSlot(i, newItem);
                else
                    Debug.LogWarning("PlayerInventory.AddItem: toolbar is not assigned!");

                return true;
            }
        }

        Debug.Log("Inventory full!");
        return false;
    }

    public void RemoveItem(int index)
    {
        if (index < 0 || index >= items.Length) return;

        items[index] = null;
        if (toolbar != null)
            toolbar.UpdateSlot(index, null);
    }

    public Item GetSelectedItem()
    {
        return items[selectedSlot];
    }

    public void SetSelectedSlot(int index)
    {
        selectedSlot = index;
        if (toolbar != null)
            toolbar.HighlightSlot(index);
    }
}
