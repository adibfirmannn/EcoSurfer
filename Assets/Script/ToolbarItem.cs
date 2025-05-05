using UnityEngine;
using UnityEngine.UI;

public class ToolbarItem : MonoBehaviour
{
    public Image itemImage;

    public void SetItem(Sprite sprite)
    {
        itemImage.sprite = sprite;
        itemImage.enabled = (sprite != null);
    }

    public void SetHighlight(bool isSelected)
    {
        itemImage.color = isSelected ? Color.yellow : Color.white;
    }

    public void ClearItem()
    {
        itemImage.sprite = null;
        itemImage.enabled = false;
    }
}
