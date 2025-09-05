using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFoodSlot : UIBase
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Button button;
    private FoodSlot _foodSlot;

    private void Start()
    {
        button.onClick.AddListener(OnClickButton);
    }

    public void SetFood(FoodSlot data)
    {
        _foodSlot = data;
        SetData();
    }

    private void SetData()
    {
        icon.sprite = _foodSlot.foodData.Sprite;
        nameText.text = _foodSlot.foodData.Name;
        countText.text = _foodSlot.count.ToString();
        countText.text += " 개";
        var price = (int)(_foodSlot.foodData.Price * RestaurantManager.Instance.revenueMultiplier);
        priceText.text = price.ToString();
        priceText.text += " G";
        descriptionText.text = _foodSlot.foodData.Description;
    }

    private void OnClickButton()
    {
        if (_foodSlot.count > 0)
        {
            RestaurantManager.Instance.AddItemForSale(_foodSlot);
            InventoryManager.Instance.UseItem(_foodSlot);
            
            RefreshCountText();
        }
        AudioManager.Instance.PlayClickSoundEffect();
    }

    private void RefreshCountText()
    {
        countText.text = _foodSlot.count.ToString();
        countText.text += " 개";
    }
}
