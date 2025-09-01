using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour , IShopObserver, IItemCollector
{
    public List<ItemData> ingredients = new();
    public List<ItemData> foods = new();

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
        if (item.IsRawIngredients)
        {
            foreach (var ingredient in ingredients)
            {
                if (ingredient.ID == item.ID)
                {
                    ingredient.Count += item.Count;
                }
                else
                {
                    ingredients.Add(item);
                }
            }
        }
        else
        {
            foreach (var food in foods)
            {
                if (food.ID == item.ID)
                {
                    food.Count += item.Count;
                }
                else
                {
                    foods.Add(item);
                }
            }
        }
    }

    public void SendFoodsToShop()
    {
        ShopManager.Instance.AddItemForSale(foods);
        foods.Clear();
    }

    public bool TryPurchase(int price)
    {
        if (price > this.money)
            return false;
        this.money -= price;
        return true;
    }

    public void OnItemSold(int price)
    {
        money += price;
    }


}