using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "fooditem", menuName = "Inventory/Items/Food")]

public class FoodItem : ItemScriptableObject
{
    public float healamount;

    private void Start()
    {
       itemType = ItemType.Food;
    }
}