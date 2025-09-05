using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [field: SerializeField]
    //public Player Player { get; private set; }
    public bool isFirstBoot = true;

    public int shopLevel = 1;

    public int restaurantLevel = 1;

    public int kitchenLevel = 1;

    public List<IShopLevelObserver> ShopLevelObservers = new();
    
    // public void SetPlayer()
    // {
    //     Player = FindAnyObjectByType<Player>();
    // }

    public void ShopLevelUp()
    {
        if (kitchenLevel >= restaurantLevel)
        {
            shopLevel = restaurantLevel;
        }

        else
        {
            shopLevel = kitchenLevel;
        }

        NotifyObserver();
    }
    public void RestaurantLevelUp()
    {
        if (restaurantLevel >= 3) return;
        restaurantLevel++;
        ShopLevelUp();
    }
    public void KitchenLevelUp()
    {
        if (kitchenLevel >= 3) return;
        kitchenLevel++;
        ShopLevelUp();
    }
    public void GamePaused()
    {
        Time.timeScale = 0.0f;
    }
    
    public void GameResume()
    {
        Time.timeScale = 1.0f;
    }
    public void GameOver()
    {
        //게임오버
        
    }

    public void SaveGame()
    {
        
    }
    
    public void AddObserver(IShopLevelObserver observer)
    {
        Debug.Log("AddObserver");
        ShopLevelObservers.Add(observer);
    }
    public void RemoveObserver(IShopLevelObserver observer)
    {
        ShopLevelObservers.Remove(observer);
    }

    public void NotifyObserver()
    {
        foreach (var observer in ShopLevelObservers)
            observer.OnLevelUp();
    }
}
