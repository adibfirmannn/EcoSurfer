using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory Settings")]
    [SerializeField] private int maxSlots = 6;

    private List<ItemData> items = new();
    private ToolbarManager toolbarManager;

    private void Awake()
    {
        // Ganti FindObjectOfType yang obsolete
        toolbarManager = Object.FindFirstObjectByType<ToolbarManager>();
        if (toolbarManager == null)
            Debug.LogError("PlayerInventory: ToolbarManager tidak ditemukan di scene!");
    }

    /// <summary>
    /// Tambah item baru, dan langsung update UI lewat ToolbarManager.AddItem(...)
    /// </summary>
    public void AddItem(ItemData newItem)
    {
        if (items.Count >= maxSlots)
        {
            Debug.Log("PlayerInventory: Inventory penuh!");
            return;
        }

        items.Add(newItem);
        toolbarManager.AddItem(newItem);
    }

    /// <summary>
    /// Hapus item pada slot yang sedang dipilih (public selectedSlot di ToolbarManager)
    /// </summary>
    public void RemoveSelectedItem()
    {
        int slot = toolbarManager.selectedSlot;
        // Hapus dulu di UI
        toolbarManager.RemoveSelectedItem();
        // Sinkronkan list internal
        if (slot >= 0 && slot < items.Count)
            items.RemoveAt(slot);
    }

    /// <summary>
    /// (Opsional) Ambil data item dari slot yang sedang dipilih
    /// </summary>
    public ItemData GetSelectedItem()
    {
        return toolbarManager.GetSelectedItem();
    }
}
