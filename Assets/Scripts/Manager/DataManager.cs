using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class DataManager : Singleton<DataManager>
{
    
    private string _defaultDataPath = Application.streamingAssetsPath;

    private readonly List<DropItemData> _dropItemDatas = new();
    public Dictionary<int, DropItemData> DropItemDatas = new();

    private readonly List<RecipeData> _recipeDatas = new();
    public Dictionary<int, RecipeData> RecipeDatas = new();
    
    [SerializeField] private List<ItemData> itemDatas = new();
    public Dictionary<int, ItemData> ItemDatas = new();
    protected override void Awake()
    {
         base.Awake();
         LoadData(_dropItemDatas, "DropItemData.json");
         LoadData(_recipeDatas, "RecipeData.json");
         LoadItemDataDict();
         LoadDropItemDataDict();
         LoadRecipeDataDict();
    }

    private void LoadDropItemDataDict()
    {
        foreach (var item in _dropItemDatas)
        {
            DropItemDatas.Add(item.ID, item);
        }
    }

    private void LoadRecipeDataDict()
    {
        foreach (var item in _recipeDatas)
        {
            RecipeDatas.Add(item.ID, item);
        }
    }
    private void LoadItemDataDict()
    {
        foreach (var item in itemDatas)
        {
            ItemDatas.Add(item.ID, item);
        }
    }
    private List<T> LoadJsonData<T>(string path)
    {
        string json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<List<T>>(json);
    }
    private void LoadData<T>(List<T> list, string additionalPath)
     {
         if(!File.Exists(Path.Combine(_defaultDataPath, additionalPath)))
         {
              Debug.LogError($"Data file not found, path: {_defaultDataPath + additionalPath}");
         }
         else{
              List<T> datas = LoadJsonData<T>(Path.Combine(_defaultDataPath, additionalPath));
              foreach (var data in datas)
              {
                  list.Add(data);
              }
         }
     }
}