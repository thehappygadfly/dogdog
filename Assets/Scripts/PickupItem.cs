using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public Inventory.InventoryItem itemType;
    public int itemAmount = 1;

    private bool pickup = false;

    private void OnTriggerEnter(Collider other)
    {
        if (pickup)
            return;

        // pick up item
        Inventory inventory = other.GetComponent<Inventory>();
        inventory.GetItem(itemType, itemAmount);

        pickup = true;
        Destroy(gameObject);

    }
}
