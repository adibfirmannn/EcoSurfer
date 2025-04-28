using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public ToolbarManager toolbarManager;

    private void OnTriggerEnter(Collider other)
    {
        TrashItem trash = other.GetComponent<TrashItem>();
        if (trash != null)
        {
            toolbarManager.AddItem(trash.itemData);
            Destroy(other.gameObject);
        }
    }
}
