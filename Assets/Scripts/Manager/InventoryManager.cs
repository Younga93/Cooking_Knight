using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager: Singleton<InventoryManager>
{
    [HideInInspector] public Inventory inventory;

    public int GetItemCount(int ID)
    {
        foreach (ItemSlot item in inventory.ingredients)
        {
            if (item.itemData.ID == ID)
            {
                return item.count;
            }
        }
        return 0;
    }

    public bool TryUseItem(int ID, int amount)
    {
        foreach (var item in inventory.ingredients)
        {
            if (item.itemData.ID == ID && item.count>=amount)
            {
                item.count -= amount;
                return true;
            }
        }

        return false;
    }

    public void UseItem(int ID, int amount)
    {
        foreach (var item in inventory.ingredients)
        {
            if (item.itemData.ID == ID && item.count>=amount)
            {
                item.count -= amount;
            }
        }
    }
}
