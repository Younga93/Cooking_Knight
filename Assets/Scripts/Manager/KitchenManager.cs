using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenManager : Singleton<KitchenManager>
{
    private List<FoodData> cookingQueue = new();
    private List<FoodData> cookedFoods = new();
    private bool isCooking;
    private float _elapsedTime;
    private Coroutine cookCoroutine;
    private bool _timeStarted;
    public event Action<float, float> TimeChanged;
    public event Action<bool> OnTimeStarted;
    private float _timeMultiplier = 1.0f;

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

    public void ReduceTimeMultiplier()
    {
        _timeMultiplier *= 0.9f;
    }
    private bool CheckIngredients(int itemID, int amount)
    {
        if (itemID == 0) return true;
        return InventoryManager.Instance.GetIngredientCount(itemID) >= amount;
    }

    public void AddFoodToCookingQueue(int recipeID)
    {
        var recipe = DataManager.Instance.RecipeDatas[recipeID];
        InventoryManager.Instance.UseItem(recipe.FirstDropItemID, recipe.FirstDropItemCount);
        InventoryManager.Instance.UseItem(recipe.SecondDropItemID, recipe.SecondDropItemCount);
        InventoryManager.Instance.UseItem(recipe.ThirdDropItemID, recipe.ThirdDropItemCount);
        cookingQueue.Add(DataManager.Instance.FoodDatas[recipe.FoodID]);
    }

    private void Update()
    {
        if (_timeStarted)
        {
            _elapsedTime += Time.deltaTime;
            TimeChanged?.Invoke(_elapsedTime, cookingQueue[0].CookTime);
        }
        if (isCooking) return;
        if (cookingQueue.Count == 0) return;
        
        StartCook();
    }

    private void StartCook()
    {
        if(cookCoroutine != null) return;
        isCooking = true;
        cookCoroutine = StartCoroutine(CookProcess());
    }

    private IEnumerator CookProcess()
    {
        try
        {
            Debug.Log("cookingStart");
            if (cookingQueue.Count == 0) yield break;
            _timeStarted = true;
            OnTimeStarted?.Invoke(true);
            yield return new WaitForSeconds(cookingQueue[0].CookTime * _timeMultiplier);
            cookedFoods.Add(cookingQueue[0]);
            cookingQueue.RemoveAt(0);
        }
        finally
        {
            _elapsedTime = 0;
            isCooking = false;
            _timeStarted = false;
            OnTimeStarted?.Invoke(false);
            cookCoroutine = null;
        }
    }
    public void OnEnterKitchen()
    {
        if (cookedFoods.Count != 0)
        {
            AddFoodsToInventory();
        }
    }

    private void AddFoodsToInventory()
    {
        foreach (var food in cookedFoods)
        {
            InventoryManager.Instance.AddItem(food);
        }

        cookedFoods.Clear();
    }
}