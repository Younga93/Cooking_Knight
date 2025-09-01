using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RecipeData
{
    public int ID { get; set; }
    public int CreateItemID { get; set; }
    public int FirstDropItemID { get; set; }
    public int FirstDropItemCount { get; set; }
    public int SecondDropItemID { get; set; }
    public int SecondDropItemCount { get; set; }
    public int ThirdDropItemID { get; set; }
    public int ThirdDropItemCount { get; set; }
}
