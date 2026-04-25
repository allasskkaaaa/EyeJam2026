using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Inventory/Item", order = 1)]
public class ItemData : ScriptableObject
{
    public Sprite itemThumbnail;
    public string itemName;
    public string itemDescription;
    public bool isMemory;
}
