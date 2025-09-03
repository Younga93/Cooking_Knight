using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventorySlot : UIBase
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI amountText;
    public bool isItem;
    public ItemSlot itemSlot;
    public FoodSlot foodSlot;

    public void SetData(ItemSlot slot)
    {
        isItem = true;
        itemSlot = slot;
        SetIcon();
        RefreshUI();
    }

    public void SetData(FoodSlot slot)
    {
        isItem = false;
        foodSlot = slot;
        SetIcon();
        RefreshUI();
    }

    private void SetIcon()
    {
        if (isItem)
        {
            icon.sprite = itemSlot.itemData.Sprite;
        }
        else
        {
            icon.sprite = foodSlot.foodData.Sprite;       
        }
    }
    public void RefreshUI()
    {
        if (isItem)
        {
            amountText.text = itemSlot.count.ToString();
        }
        else
        {
            amountText.text = foodSlot.count.ToString();
        }
    }
}
