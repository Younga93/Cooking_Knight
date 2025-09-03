using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour , IShopObserver, IItemCollector
{
    public List<ItemSlot> ingredients = new();
    public List<FoodSlot> foods = new();

    public int money;

    private void OnEnable()
    {
        ShopManager.Instance.AddObserver(this);
    }
    private void OnDisable()
    {
        ShopManager.Instance.RemoveObserver(this);
    }
    
    public void AddItem(ItemData item)
    {
        foreach (var ingredient in ingredients)
        {
            if (ingredient.itemData.ID == item.ID)
            {
                ingredient.count++;
                return;
            }
        }
        var itemSlot = new ItemSlot(item);
        ingredients.Add(itemSlot);
        itemSlot.count = 1;
    }

    public void AddItem(FoodData item)
    {
        foreach (var food in foods)
        {
            if (food.foodData.ID == item.ID)
            {
                food.count++;
                return;
            }
        }
        var foodSlot = new FoodSlot(item);
        foods.Add(foodSlot);
        foodSlot.count = 1;
    }


    public void SendFoodsToShop()
    {
        ShopManager.Instance.AddItemForSale(foods);
        foods.Clear();
    }

    public bool TryPurchase(int price)
    {
        if (price > money)
            return false;
        money -= price;
        return true;
    }

    public void OnItemSold(int price)
    {
        money += price;
    }
}