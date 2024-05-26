using PixelCrushers.QuestMachine.Demo;
using UnityEngine;

public class HerbCollector : MonoBehaviour
{
    public Inventory inventory; // Ссылка на инвентарь

    public void CollectNearbyItems(Collider hitCollider)
    { 
        if (hitCollider.CompareTag("Herb1"))
        {
            inventory.AddItem(Inventory.Herb1Slot);
            Destroy(hitCollider.gameObject);
        }
        else if (hitCollider.CompareTag("Herb2"))
        {
            inventory.AddItem(Inventory.Herb2Slot);
            Destroy(hitCollider.gameObject);
        }
        else if (hitCollider.CompareTag("Herb3"))
        {
            inventory.AddItem(Inventory.Herb3Slot);
            Destroy(hitCollider.gameObject);
        }
    }
}
