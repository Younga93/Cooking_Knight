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
}
