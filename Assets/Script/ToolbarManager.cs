using UnityEngine;

public class ToolbarManager : MonoBehaviour
{
    public ToolbarSlot[] slots;

    // ToolbarManager.cs
    //public void UpdateSlot(int index, Item item)
    //{
    //    if (index >= 0 && index < slots.Length)
    //    {
    //        if (slots[index] != null)
    //        {
    //            slots[index].SetItem(item);  // Ensure toolbarSlot is not null
    //        }
    //        else
    //        {
    //            Debug.LogError($"Toolbar slot {index} is null.");
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError($"Index {index} is out of bounds for toolbarSlots.");
    //    }
    //}


    //public void HighlightSlot(int index)
    //{
    //    for (int i = 0; i < slots.Length; i++)
    //    {
    //        slots[i].transform.localScale = (i == index) ? Vector3.one * 1.2f : Vector3.one;
    //    }
    //}
}
