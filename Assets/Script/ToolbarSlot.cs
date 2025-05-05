using UnityEngine;
using UnityEngine.UI;

public class ToolbarSlot : MonoBehaviour
{
    public Image icon;

    public void SetItem(Item item)
    {
        if (item != null)
        {
            icon.sprite = item.icon;
            icon.enabled = true;
        }
        else
        {
            icon.sprite = null;
            icon.enabled = false;
        }
    }
}
