using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public Inventory inventory;
    public Text RepairNum;
    public Text EnergyNum;

    void Start()
    {
        inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        RepairNum = GameObject.Find("RepairNum").GetComponent<Text>();
        EnergyNum = GameObject.Find("EnergyNum").GetComponent<Text>();

    }

    void Update()
    {
        RepairNum.text = inventory.GetItemCount(Inventory.InventoryItem.REPAIRKIT).ToString();
        EnergyNum.text = inventory.GetItemCount(Inventory.InventoryItem.ENERGYPACK).ToString();
    }

    public void OnRepairButtonClikck()
    {
        inventory.UseItem(Inventory.InventoryItem.REPAIRKIT, 1);
    }

    public void OnEnergyButtonClikck()
    {
        inventory.UseItem(Inventory.InventoryItem.ENERGYPACK, 1 );
    }

}
