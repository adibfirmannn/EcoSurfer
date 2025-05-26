using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Inventory Settings")]
    public int maxSlots = 5;
    public TrashSlot[] trashSlots;

    [Header("UI References")]
    public GameObject[] toolbarSlotUI; // Reference ke Slot GameObjects (opsional)
    public Image[] slotIcons;
    public Text[] slotCounts; // Jika ingin menampilkan jumlah (optional)

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            InitializeInventory();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeInventory()
    {
        trashSlots = new TrashSlot[maxSlots];
        for (int i = 0; i < maxSlots; i++)
        {
            trashSlots[i] = new TrashSlot();
        }

        UpdateToolbarUI();
    }

    public bool AddItem(ItemData item)
    {
        // Cari slot kosong
        for (int i = 0; i < trashSlots.Length; i++)
        {
            if (trashSlots[i].CanAcceptItem())
            {
                trashSlots[i].SetItem(item);
                UpdateToolbarUI();
                return true;
            }
        }

        return false; // Inventory penuh
    }

    public bool RemoveItem(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < trashSlots.Length && !trashSlots[slotIndex].isEmpty)
        {
            trashSlots[slotIndex].ClearSlot();
            UpdateToolbarUI();
            return true;
        }

        return false;
    }

    public TrashSlot[] GetItemsByCategory(ItemType category)
    {
        System.Collections.Generic.List<TrashSlot> matchingSlots = new System.Collections.Generic.List<TrashSlot>();

        for (int i = 0; i < trashSlots.Length; i++)
        {
            if (!trashSlots[i].isEmpty && trashSlots[i].currentItem.itemType == category)
            {
                matchingSlots.Add(trashSlots[i]);
            }
        }

        return matchingSlots.ToArray();
    }

    void UpdateToolbarUI()
    {
        for (int i = 0; i < trashSlots.Length; i++)
        {
            if (i < slotIcons.Length)
            {
                if (slotIcons[i] == null)
                {
                    Debug.LogWarning($"slotIcons[{i}] is null or destroyed!");
                    continue;
                }

                if (trashSlots[i].isEmpty)
                {
                    slotIcons[i].sprite = null;
                    slotIcons[i].color = new Color(1, 1, 1, 0.3f); // Transparan untuk slot kosong
                }
                else
                {
                    slotIcons[i].sprite = trashSlots[i].currentItem.itemSprite;
                    slotIcons[i].color = Color.white;
                }
            }
        }
    }

    public int GetTotalItems()
    {
        int count = 0;
        foreach (var slot in trashSlots)
        {
            if (!slot.isEmpty) count++;
        }
        return count;
    }

    public bool IsInventoryFull()
    {
        return GetTotalItems() >= maxSlots;
    }

    void Update()
    {
        // Debug keys
        if (Input.GetKeyDown(KeyCode.I))
        {
            // Debug inventory status
            Debug.Log($"=== INVENTORY STATUS ===");
            Debug.Log($"Total Items: {GetTotalItems()}/{maxSlots}");

            for (int i = 0; i < trashSlots.Length; i++)
            {
                if (!trashSlots[i].isEmpty)
                {
                    Debug.Log($"Slot {i}: {trashSlots[i].currentItem.itemName} ({trashSlots[i].currentItem.itemType})");
                }
                else
                {
                    Debug.Log($"Slot {i}: EMPTY");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            // Clear inventory untuk testing
            for (int i = 0; i < trashSlots.Length; i++)
            {
                RemoveItem(i);
            }
            Debug.Log("Inventory cleared!");
        }
    }
}
