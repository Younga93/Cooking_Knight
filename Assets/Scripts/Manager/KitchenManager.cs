using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenManager : Singleton<KitchenManager>
{
    private List<FoodData> cookingQueue = new();
    private List<FoodData> cookedFoods = new();
    private bool isCooking = false;
    private float time = 0.0f;
    private Inventory _inventory;

    private void Start()
    {
        _inventory = InventoryManager.Instance.inventory;
    }

    public bool IsCookAvailable(int recipeID)
    {
        var recipe = DataManager.Instance.RecipeDatas[recipeID];
        if (!CheckIngredients(recipe.FirstDropItemID, recipe.FirstDropItemCount))
            return false;
        if (!CheckIngredients(recipe.SecondDropItemID, recipe.SecondDropItemCount))
            return false;
        if (!CheckIngredients(recipe.ThirdDropItemID, recipe.ThirdDropItemCount))
            return false;
        
        return true;
    }

    private bool CheckIngredients(int itemID, int amount)
    {
        return itemID != 0 && InventoryManager.Instance.GetItemCount(itemID) >= amount;
    }

    public void Cook(int recipeID)
    {
        var recipe = DataManager.Instance.RecipeDatas[recipeID];
        InventoryManager.Instance.UseItem(recipe.FirstDropItemID, recipe.FirstDropItemCount);
        InventoryManager.Instance.UseItem(recipe.SecondDropItemID, recipe.SecondDropItemCount);
        InventoryManager.Instance.UseItem(recipe.ThirdDropItemID, recipe.ThirdDropItemCount);
        cookingQueue.Add(DataManager.Instance.FoodDatas[recipe.FoodID]);
        isCooking = true;
    }

    private void Update()
    {
        if (isCooking)
        {
            time += Time.deltaTime;
        }
    }

    public void OnEnterKitchen()
    {
        if (cookingQueue.Count > 0)
        {
            foreach (FoodData food in cookingQueue)
            {
                //현재 foodData에 time이 없어 주석처리 함.
                // if (time >= food.CookTime)
                // {
                //     cookedFoods.Add(food);
                //     cookingQueue.Remove(food);
                //     time -= food.CookTime;
                // }
                // else
                // {
                //     break;
                // }
            }

            if (cookingQueue.Count == 0)
            {
                isCooking = false;
            }

            if (cookedFoods.Count != 0)
            {
                AddFoodsToInventory();
            }
        }
    }

    private void AddFoodsToInventory()
    {
        foreach (var food in cookedFoods)
        {
            _inventory.AddItem(food);
        }
        cookedFoods.Clear();
    }
}
