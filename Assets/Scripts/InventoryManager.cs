using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject crosshair;
    public GameObject UIPanel;
    public bool isOpened;
    public Transform inventoryPanel;
    public List<InventorySlot>  slots = new List<InventorySlot> ();
    private Camera mainCamera;
    public float reachDistance = 3;


    private void Awake()
    {
        UIPanel.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {

        mainCamera = GameObject.Find("Camera").GetComponent<Camera>();

        for (int i = 0; i < inventoryPanel.childCount; i++)
        {
            if (inventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null) 
            {
                slots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
            }
        }

        UIPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {   
            isOpened = !isOpened;

            if (isOpened)
            {
               UIPanel.SetActive(true);
               crosshair.SetActive(false);
            }
            else 
            {
                UIPanel.SetActive(false);
                crosshair.SetActive(true);
            }
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, reachDistance)) 
        {   
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                Debug.DrawRay(ray.origin, ray.direction * reachDistance, Color.green);
                if (hit.collider.gameObject.GetComponent<Item>() != null)
                {
                    AddItem(hit.collider.gameObject.GetComponent<Item>().item, hit.collider.gameObject.GetComponent<Item>().amount);
                    Destroy(hit.collider.gameObject);
                }
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * reachDistance, Color.green);
            }
        }
    }
    private void AddItem(ItemScriptableObject _item, int _amount) 
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.item == _item)
            {
                slot.amount += _amount;
                slot.itemamount_text.text = slot.amount.ToString(); 
                return;
            }
        }
        foreach(InventorySlot slot in slots)
        {
            if (slot.isEmpty == false)
            {
                slot.item = _item;
                slot.amount = _amount;
                slot.isEmpty = false;
                slot.SetIcon(_item.icon);
                slot.itemamount_text.text = _amount.ToString(); 
                break;
            }
        }
    }
}
