using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/ItemData")]
public class ItemData : ScriptableObject
{
    public int ID;
    public string Name;
    public Sprite Sprite;
    public GameObject Prefab;
}