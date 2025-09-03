using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour , IShopObserver, IItemCollector
{
    public List<ItemData> ingredients = new();
    public Dictionary<int, ItemData> ingredientDict = new();
    public List<FoodData> foods = new();

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
            if (ingredient.ID == item.ID)
            {
                ingredient.Count++;
                return;
            }
        }
        ingredients.Add(item);
    }

    public void AddItem(FoodData item)
    {
        foreach (var food in foods)
        {
            if (food.ID == item.ID)
            {
                food.Count++;
                return;
            }
        }
        foods.Add(item);
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