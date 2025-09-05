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
        RestaurantManager.Instance.AddItemForSale(foods);
        foods.Clear();
    }

    


}