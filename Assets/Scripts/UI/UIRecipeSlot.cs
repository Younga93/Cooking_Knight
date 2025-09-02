using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRecipeSlot : UIBase
{
    private int index;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button button;
    public Outline outline;
    private UIKitchen _uiKitchen;
    public RecipeData recipeData;

    private void Start()
    {
        button.onClick.AddListener(OnClickButton);
    }

    public void SetRecipe(RecipeData data, UIKitchen uiKitchen)
    {
        _uiKitchen = uiKitchen;
        recipeData = data;
        index = recipeData.ID;
        outline = GetComponent<Outline>();
        outline.enabled = false;
        SetData();
    }

    private void SetData()
    {
        var foodData = DataManager.Instance.FoodDatas[recipeData.FoodID];
        nameText.text = foodData.Name;
        descriptionText.text = foodData.Description;
        priceText.text = foodData.Price.ToString();
        priceText.text += " G";
    }
    private void OnClickButton() => _uiKitchen.SelectRecipe(index);
}
