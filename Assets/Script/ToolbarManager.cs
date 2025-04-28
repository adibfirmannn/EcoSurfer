using UnityEngine;
using UnityEngine.UI;

public class ToolbarManager : MonoBehaviour
{
    public Image[] itemImages; // Drag ToolbarItem (6 slot)
    private ItemData[] itemsInSlots = new ItemData[6];

    public int selectedSlot = 0;

    private void Update()
    {
        // Pilih slot dengan tombol 1 - 6
        for (int i = 0; i < 6; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectSlot(i);
            }
        }
    }

    public void AddItem(ItemData item)
    {
        for (int i = 0; i < itemsInSlots.Length; i++)
        {
            if (itemsInSlots[i] == null)
            {
                itemsInSlots[i] = item;
                UpdateUI();
                return;
            }
        }
        Debug.Log("Inventory penuh!");
    }

    void UpdateUI()
    {
        for (int i = 0; i < itemImages.Length; i++)
        {
            if (itemsInSlots[i] != null)
            {
                itemImages[i].sprite = itemsInSlots[i].icon;
                itemImages[i].enabled = true;
            }
            else
            {
                itemImages[i].enabled = false;
            }
        }
    }

    void SelectSlot(int index)
    {
        selectedSlot = index;
        UpdateSlotHighlight();
    }

    void UpdateSlotHighlight()
    {
        for (int i = 0; i < itemImages.Length; i++)
        {
            if (i == selectedSlot)
            {
                itemImages[i].transform.localScale = Vector3.one * 1.2f; // Membesar
            }
            else
            {
                itemImages[i].transform.localScale = Vector3.one; // Normal
            }
        }
    }

    public ItemData GetSelectedItem()
    {
        return itemsInSlots[selectedSlot];
    }

    public void RemoveSelectedItem()
    {
        itemsInSlots[selectedSlot] = null;
        UpdateUI();
    }
}
