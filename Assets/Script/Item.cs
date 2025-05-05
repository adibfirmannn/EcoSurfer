using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string category; // Misalnya: Plastik, Kertas, Organik
    public Sprite icon;
}


