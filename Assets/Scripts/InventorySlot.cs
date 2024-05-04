using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public ItemScriptableObject item;
    public int amount;
    public bool isEmpty = true;
    public GameObject iconGO;
    public TMP_Text itemamount_text;

    private void Awake()
    {
        iconGO = transform.GetChild(0).gameObject;
        itemamount_text = transform.GetChild(1).GetComponent < TMP_Text> ();

    }

    public void SetIcon(Sprite icon)
    {
        iconGO.GetComponent<Image>().color = new Color(1,1,1,1);
        iconGO.GetComponent<Image>().sprite = icon;
    }
}
