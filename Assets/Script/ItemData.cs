using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public TrashType trashType; // Organik, Anorganik, etc
}

public enum TrashType
{
    Organic,
    Inorganic,
    Hazardous,
    Other
}
