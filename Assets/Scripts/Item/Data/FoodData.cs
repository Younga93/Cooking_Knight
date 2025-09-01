using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "Items/FoodData")]
public class FoodData : ScriptableObject
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }
    public Sprite Sprite { get; set; }
}