using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class DataManager : Singleton<DataManager>
{
    private string _defaultDataPath = Application.streamingAssetsPath;

    private readonly List<DropItemTable> _dropItemTables = new();
    public Dictionary<int, DropItemTable> DropItemTables = new();
    
    private readonly List<RecipeData> _recipeDatas = new();
    public Dictionary<int, RecipeData> RecipeDatas = new();

    [SerializeField] private List<ItemData> itemDatas = new();
    [SerializeField] private List<FoodData> foodDatas = new();
    public Dictionary<int, ItemData> ItemDatas = new();
    public Dictionary<int, FoodData> FoodDatas = new();

    protected override void Awake()
    {
        base.Awake();
        LoadData(_recipeDatas, "RecipeDataJson.json");
        LoadData(_dropItemTables, "DropItemTableDataJson.json");
        
        LoadItemDataDict();
        LoadFoodDataDict();
        LoadDropItemTableDict();
        LoadRecipeDataDict();
    }
    

    private void LoadDropItemTableDict()
    {
        foreach (var item in _dropItemTables)
        {
            DropItemTables.Add(item.ID, item);
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
        GameObject go = ResourceManager.Instance.Create<GameObject>(Constants.DataHolder + "ItemDataHolder", this.transform);
        ItemDataHolder holder = go.GetComponent<ItemDataHolder>();
        foreach (ItemData data in holder.itemDataList)
        {
            ItemData itemData = ScriptableObject.CreateInstance<ItemData>();
            itemData.ID = data.ID;
            itemData.Name = data.Name;
            itemData.Sprite = data.Sprite;
            itemData.Prefab = data.Prefab;
            itemDatas.Add(itemData);
        }
        foreach (var item in itemDatas)
        {
            ItemDatas.Add(item.ID, item);
        }

        Destroy(go);
    }

    private void LoadFoodDataDict()
    {
        GameObject go = ResourceManager.Instance.Create<GameObject>(Constants.DataHolder + "FoodDataHolder", this.transform);
        FoodDataHolder holder = go.GetComponent<FoodDataHolder>();
        foreach (FoodData data in holder.foodDataList)
        {
            FoodData foodData = ScriptableObject.CreateInstance<FoodData>();
            foodData.ID = data.ID;
            foodData.Name = data.Name;
            foodData.Description = data.Description;
            foodData.Price = data.Price;
            foodData.CookTime = data.CookTime;
            foodData.SellTime = data.SellTime;
            foodData.Sprite = data.Sprite;
            foodDatas.Add(foodData);
        }
        foreach (var food in foodDatas)
        {
            FoodDatas.Add(food.ID, food);
        }
        Destroy(go);
    }

    private List<T> LoadJsonData<T>(string path)
    {
        string json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<List<T>>(json);
    }

    private void LoadData<T>(List<T> list, string additionalPath)
    {
        if (!File.Exists(Path.Combine(_defaultDataPath, additionalPath)))
        {
            Debug.LogError($"Data file not found, path: {_defaultDataPath + additionalPath}");
        }
        else
        {
            List<T> datas = LoadJsonData<T>(Path.Combine(_defaultDataPath, additionalPath));
            foreach (var data in datas)
            {
                list.Add(data);
            }
        }
    }

    //매개변수에 담은 DropTable의 리스트에 따라 드롭될 아이템의 확률 계산을 하고, 등장할 아이템을 droppedItems에 담습니다.
    //사용하실 때, boolean 값으로 아이템이 드롭 됐는지 확인 후, droppedItems에 있는 ItemData.Prefab을 활용하시길 바랍니다.
    public bool GetDroppedItem(int dropTable, out GameObject droppedItems)
    {
        float total = 0f;
        foreach (var drop in DropItemTables[dropTable].DropItemDatas)
        {
            total += drop.Percent;
        }

        float random = UnityEngine.Random.value * total;
        int id = 0;
        foreach (var drop in DropItemTables[dropTable].DropItemDatas)
        {
            random -= drop.Percent;
            if (random <= 0f)
            {
                id = drop.ID;
                break;
            }
        }

        if (id == 0)
        {
            droppedItems = null;
            return false;
        }

        droppedItems = ItemDatas[id].Prefab;
        return true;
    }
}