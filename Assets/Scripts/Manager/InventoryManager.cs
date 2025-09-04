using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class InventoryManager: Singleton<InventoryManager>, IShopObserver
{
    [HideInInspector] public Inventory inventory;

    public event Action OnMoneyChanged;
    public event Action<bool, bool, int> OnItemAdded;
    public event Action<bool, bool, int> OnItemUsed;

    protected override void Awake()
    {
        base.Awake();
        inventory = gameObject.AddComponent<Inventory>();
    }

    [CanBeNull] public ItemSlot GetItem(int id)
    {
        foreach (var item in inventory.ingredients)
        {
            if (item.itemData.ID == id)
            {
                return item;
            }
        }
        return null;
    }

    public void AddItem(ItemData item)
    {
        foreach (var ingredient in inventory.ingredients)
        {
            if (ingredient.itemData.ID == item.ID)
            {
                ingredient.count++;
                OnItemAdded?.Invoke(false, true, item.ID);
                return;
            }
        }
        var itemSlot = new ItemSlot(item);
        inventory.ingredients.Add(itemSlot);
        itemSlot.count = 1;
        OnItemAdded?.Invoke(true, true, item.ID);
    }

    public void AddItem(FoodData item)
    {
        foreach (var food in inventory.foods)
        {
            if (food.foodData.ID == item.ID)
            {
                food.count++;
                OnItemAdded?.Invoke(false, false, item.ID);
                return;
            }
        }
        var foodSlot = new FoodSlot(item);
        inventory.foods.Add(foodSlot);
        foodSlot.count = 1;
        OnItemAdded?.Invoke(true, false, item.ID);
    }
    
    [CanBeNull]
    public FoodSlot GetFood(int id)
    {
        foreach (var food in inventory.foods)
        {
            if (food.foodData.ID == id)
            {
                return food;
            }
        }

        return null;
    }
    private void OnEnable()
    {
        RestuarantManager.Instance.AddObserver(this);
    }

    public int GetIngredientCount(int id)
    {
        foreach (var item in inventory.ingredients)
        {
            if (item.itemData.ID == id)
            {
                return item.count;
            }
        }
        return 0;
    }
    private void OnDisable()
    {
        if (RestuarantManager.Instance == null)
        {
            return;
        }
        RestuarantManager.Instance.RemoveObserver(this);
    }
    
    public void OnItemSold(int price)
    {
        inventory.money += price;
        OnMoneyChanged?.Invoke();
    }
    
    public int GetItemCount(ItemSlot itemSlot)
    {
        foreach (ItemSlot item in inventory.ingredients)
        {
            if (item == itemSlot)
            {
                return item.count;
            }
        }
        return 0;
    }
    
    public int GetItemCount(FoodSlot foodSlot)
    {
        foreach (FoodSlot food in inventory.foods)
        {
            if (food == foodSlot)
            {
                return food.count;
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
        ItemSlot itemSlot = null;
        foreach (var item in inventory.ingredients)
        {
            if (item.itemData.ID == ID && item.count>=amount)
            {
                item.count -= amount;
                itemSlot = item;
                break;
            }
        }
        
        if (itemSlot != null && itemSlot.count <= 0)
        {
            inventory.ingredients.Remove(itemSlot);
            OnItemUsed?.Invoke(true, true, ID);
        }
        else
        {
            OnItemUsed?.Invoke(false, true, ID);
        }
    }

    public void UseItem(FoodSlot foodSlot)
    {
        foreach (var food in inventory.foods)
        {
            if (food == foodSlot)
            {
                food.count--;
                break;
            }
        }

        if (foodSlot.count <= 0)
        {
            inventory.foods.Remove(foodSlot);
            OnItemUsed?.Invoke(true, false, foodSlot.foodData.ID);
        }
        else
        {
            OnItemUsed?.Invoke(false, false, foodSlot.foodData.ID);       
        }
    }
}
