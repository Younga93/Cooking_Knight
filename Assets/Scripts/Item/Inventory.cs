using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemSlot> ingredients = new();
    public List<FoodSlot> foods = new();

    public int money;
    
    


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


}