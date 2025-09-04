using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : UIBase
{
    [SerializeField] private VerticalLayoutGroup layoutGroup;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private float heightChange;
    private List<UIInventorySlot> _inventorySlots = new();
    protected override void OnOpen()
    {
        RefreshMoneyText();
        CreateSlots();
        InventoryManager.Instance.OnMoneyChanged += RefreshMoneyText;
        InventoryManager.Instance.OnItemAdded += OnItemAdded;
        InventoryManager.Instance.OnItemUsed += OnItemUsed;
    }

    protected override void OnClose()
    {
        InventoryManager.Instance.OnMoneyChanged -= RefreshMoneyText;
        InventoryManager.Instance.OnItemAdded -= OnItemAdded;
        InventoryManager.Instance.OnItemUsed -= OnItemUsed;
    }

    private void CreateSlots()
    {
        foreach (ItemSlot item in InventoryManager.Instance.inventory.ingredients)
        {
            ChangeHeight(heightChange);
            CreateNewSlot(item);
        }

        foreach (FoodSlot food in InventoryManager.Instance.inventory.foods)
        {
            ChangeHeight(heightChange);
            CreateNewSlot(food);
        }
    }
    private void RefreshMoneyText()
    {
        moneyText.text = InventoryManager.Instance.inventory.money.ToString();
    }

    private void ChangeHeight(float height)
    {
        var rt = layoutGroup.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y - height * 0.5f);
        var currentHeight = rt.rect.height;
        var targetHeight = currentHeight + height;
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, targetHeight);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rt);
    }
    private void OnItemAdded(bool isNewItem, bool isItem, int id)
    {
        if (isNewItem)
        {
            ChangeHeight(heightChange);
            if (isItem)
            {
                var item = InventoryManager.Instance.GetItem(id);
                CreateNewSlot(item);
            }
            else
            {
                var food = InventoryManager.Instance.GetFood(id);
                CreateNewSlot(food);
            }
        }
        else
        {
            foreach (var slot in _inventorySlots)
            {
                slot.RefreshUI();
            }
        }
    }

    private void OnItemUsed(bool isDisappeared, bool isItem, int id)
    {
        if (isDisappeared)
        {
            ChangeHeight(-heightChange);
            if (isItem)
            {
                foreach (var slot in _inventorySlots.ToList())
                {
                    if (!slot.isItem) continue;
                    if (id == slot.itemSlot.itemData.ID)
                    {
                        _inventorySlots.Remove(slot);
                        Destroy(slot.gameObject);
                        break;
                    }
                }
            }
            else
            { 
                foreach (var slot in _inventorySlots.ToList())
                {
                    if (slot.isItem) continue;
                    if (id == slot.foodSlot.foodData.ID)
                    {
                        _inventorySlots.Remove(slot);
                        Destroy(slot.gameObject);
                        break;   
                    }
                }
            }
        }
        else
        {
            foreach (var slot in _inventorySlots)
            {
                slot.RefreshUI();
            }
        }
    }

    private void CreateNewSlot(ItemSlot item)
    {
        var slot = UIManager.Instance.CreateSlotUI<UIInventorySlot>(layoutGroup.transform);
        slot.SetData(item);
        _inventorySlots.Add(slot);
    }
    
    private void CreateNewSlot(FoodSlot item)
    {
        var slot = UIManager.Instance.CreateSlotUI<UIInventorySlot>(layoutGroup.transform);
        slot.SetData(item);
        _inventorySlots.Add(slot);
    }
}