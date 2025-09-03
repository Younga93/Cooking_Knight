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
    private FoodSlot _foodData;

    private void Start()
    {
        button.onClick.AddListener(OnClickButton);
    }

    public void SetFood(FoodSlot data)
    {
        _foodData = data;
        SetData();
    }

    private void SetData()
    {
        icon.sprite = _foodData.foodData.Sprite;
        nameText.text = _foodData.foodData.Name;
        countText.text = _foodData.count.ToString();
        countText.text += " 개";
        priceText.text = _foodData.foodData.Price.ToString();
        priceText.text += " G";
        descriptionText.text = _foodData.foodData.Description;
    }

    private void OnClickButton()
    {
        if (_foodData.count > 0)
        {
            ShopManager.Instance.AddItemForSale(_foodData);
            _foodData.count--;
            RefreshCountText();
        }
    }

    private void RefreshCountText()
    {
        countText.text = _foodData.count.ToString();
        countText.text += " 개";
    }
}
