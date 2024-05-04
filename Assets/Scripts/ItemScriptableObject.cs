using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {Default, Plants, Tool, Food}

public class ItemScriptableObject : ScriptableObject
{
    public string itemname;
    public int maximumAmount;
    public string itemDescription;
    public ItemType itemType;
    public GameObject ItemPrefab;
    public Sprite icon;
}
