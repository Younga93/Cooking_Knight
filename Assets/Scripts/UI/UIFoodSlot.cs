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
    private FoodData _foodData;

    private void Start()
    {
        button.onClick.AddListener(OnClickButton);
    }

    public void SetFood(FoodData data)
    {
        _foodData = data;
        SetData();
    }

    private void SetData()
    {
        icon.sprite = _foodData.Sprite;
        nameText.text = _foodData.Name;
        countText.text = _foodData.Count.ToString();
        countText.text += " 개";
        priceText.text = _foodData.Price.ToString();
        priceText.text += " G";
        descriptionText.text = _foodData.Description;
    }

    private void OnClickButton()
    {
        if (_foodData.Count > 0)
        {
            ShopManager.Instance.AddItemForSale(_foodData);
            _foodData.Count--;
            RefreshCountText();
        }
    }

    private void RefreshCountText()
    {
        countText.text = _foodData.Count.ToString();
        countText.text += " 개";
    }
}
