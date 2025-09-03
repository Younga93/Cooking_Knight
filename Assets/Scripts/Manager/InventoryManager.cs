using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager: Singleton<InventoryManager>
{
    [HideInInspector] public Inventory inventory;

    public int GetItemCount(int ID)
    {
        foreach (ItemData item in inventory.ingredients)
        {
            if (item.ID == ID)
            {
                return item.Count;
            }
        }
        return 0;
    }

    public bool TryUseItem(int ID, int amount)
    {
        foreach (var item in inventory.ingredients)
        {
            if (item.ID == ID && item.Count>=amount)
            {
                item.Count -= amount;
                return true;
            }
        }

        return false;
    }

    public void UseItem(int ID, int amount)
    {
        foreach (var item in inventory.ingredients)
        {
            if (item.ID == ID && item.Count>=amount)
            {
                item.Count -= amount;
            }
        }
    }
}
