using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData : BaseData
{
    public string ItemName      { get { return itemName; } }
    public Sprite ItemSprite    { get { return itemSprite; } }

    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemSprite;
}

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Database/ItemDatabase")]
public class ItemDatabase : BaseDatabase<ItemData>
{
    
}
