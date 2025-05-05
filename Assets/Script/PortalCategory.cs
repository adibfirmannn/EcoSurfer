using UnityEngine;

public class PortalCategory : MonoBehaviour
{
    public string acceptedItemName;

    public bool Accepts(Sprite item)
    {
        return item != null && item.name.Contains(acceptedItemName);
    }
}
