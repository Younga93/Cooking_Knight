using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class DataManager : Singleton<DataManager>
{
    
    private string _defaultDataPath = Application.streamingAssetsPath;
    public Dictionary<int, DropItemData> DropItemDatas = new();
    //public Dictionary<int, RecipeData> RecipeDatas = new();
    //public Dictionary<int, ItemData> ItemDatas = new();
    protected override void Awake()
    {
         base.Awake();
         LoadData<DropItemData>(DropItemDatas, "DropItemData.json");
         //LoadData<RecipeData>(RecipeDatas, "RecipeData.json");
         //LoadData<ItemData>(ItemDatas, "ItemData.json");
     }
    
    private List<T> LoadJsonData<T>(string path)
    {
        string json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<List<T>>(json);
    }
    private void LoadData<T>(Dictionary<int, T> dictionary, string additionalPath)
     {
         if(!File.Exists(Path.Combine(_defaultDataPath, additionalPath)))
         {
              Debug.LogError($"Data file not found, path: {_defaultDataPath + additionalPath}");
         }
         else{
              List<T> data = LoadJsonData<T>(Path.Combine(_defaultDataPath, additionalPath));
              //foreach (var item in data)
              //{
                  //dictionary.Add(item.Id, item);
              //}
         }
     }
}