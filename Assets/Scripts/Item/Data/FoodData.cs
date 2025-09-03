using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "Items/FoodData")]
public class FoodData : ScriptableObject
{
    public int ID;
    public string Name;
    public string Description;
    public int Price;
    public float CookTime;
    public float SellTime;
    public Sprite Sprite;
}
