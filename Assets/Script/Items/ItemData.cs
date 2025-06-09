using UnityEngine;
using UnityEngine.UI;
// TAMBAHKAN BARIS INI jika ItemType masih tidak dikenali
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Item", menuName = "EcoSurfer/Item Data")]
public class ItemData : ScriptableObject
{
    [Header("Item Information")]
    public string itemName;
    public ItemType itemType;
    public Sprite itemSprite;
    public int pointValue = 5;

    [Header("Visual Effects")]
    public Color itemColor = Color.white;
    public GameObject pickupEffect;
}