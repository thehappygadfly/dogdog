using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public enum InventoryItem
    {
        ENERGYPACK,
        REPAIRKIT,
    }

    public PlayerStatus status;
    private float repairKitHealthAmt = 5.0f;
    private float energyPackHealAmt = 5.0f;

    Dictionary<InventoryItem,int> ItemDict;

    void Start()
    {
        status = GetComponent<PlayerStatus>();
        ItemDict = new Dictionary<InventoryItem, int>();

        ItemDict.Add(InventoryItem.ENERGYPACK, 1);
        ItemDict.Add(InventoryItem.REPAIRKIT, 2);

    }

    void Update()
    {
        
    }

    public void GetItem(InventoryItem item, int amount)
    {
        ItemDict[item] += amount;
    }

    public void UseItem(InventoryItem item, int amount)
    {
        if (ItemDict[item] <= 0)
        {
            return;
        }
        ItemDict[item] -= amount;

        switch (item)
        {
            case InventoryItem.ENERGYPACK:
                status.addEnergy(energyPackHealAmt);
                break;
            case InventoryItem.REPAIRKIT:
                status.AddHealth(repairKitHealthAmt);
                break;

        }
    }

    public bool CompareItemCount(InventoryItem compItem, int compNumber)
    {
        return ItemDict[compItem] >= compNumber;
    }

    public int GetItemCount(InventoryItem compItem)
    {
        return ItemDict[compItem];
    }


}
