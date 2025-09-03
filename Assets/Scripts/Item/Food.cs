using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FoodSlot
{
    public FoodData foodData;
    public int count;
    
    public FoodSlot(FoodData data)
    {
        foodData = data;
    }
}
