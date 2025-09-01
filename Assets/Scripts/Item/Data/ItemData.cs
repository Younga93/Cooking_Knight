using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/ItemData")]
public class ItemData : ScriptableObject
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Count { get; set; }
    public Sprite Sprite { get; set; }
    public GameObject Prefab { get; set; }
}