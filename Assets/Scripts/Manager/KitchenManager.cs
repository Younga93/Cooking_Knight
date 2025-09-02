using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenManager : Singleton<KitchenManager>
{
    
    private float time = 0.0f;
    private Inventory _inventory;

    private void Start()
    {
        _inventory = InventoryManager.Instance.inventory;
    }

    public bool TryCook(int recipeID)
    {
        return false;
    }
    
    
}
