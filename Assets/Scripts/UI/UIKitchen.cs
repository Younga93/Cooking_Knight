using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIKitchen : UIBase
{
    [SerializeField] private VerticalLayoutGroup layoutGroup;
    private Transform _contents;
    [HideInInspector] public int currentID;
    private List<UIRecipeSlot> _recipeSlots;
    [SerializeField] private Image image1;
    [SerializeField] private Image image2;
    [SerializeField] private Image image3;
    [SerializeField] private TextMeshProUGUI text1;
    [SerializeField] private TextMeshProUGUI text2;
    [SerializeField] private TextMeshProUGUI text3;
    [SerializeField] private Button button;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        _contents = layoutGroup.GetComponent<Transform>();
        currentID = -1;
    }

    protected override void OnOpen()
    {
        foreach (var data in DataManager.Instance.RecipeDatas.Values)
        {
            IncreaseHeight(200);
            var slot = UIManager.Instance.CreateSlotUI<UIRecipeSlot>(_contents);
            slot.SetRecipe(data, this);
            _recipeSlots.Add(slot);
        }
    }

    private void IncreaseHeight(float height)
    {
        var rt = _contents.GetComponent<RectTransform>();
        var currentHeight = rt.rect.height;
        var targetHeight = currentHeight + height;
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, targetHeight);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rt);
    }

    public void SelectRecipe(int index)
    {
        currentID = index;
        RefreshUI();
    }

    private void RefreshUI()
    {
        foreach (var slot in _recipeSlots)
        {
            if (slot.recipeData.ID == currentID)
            {
                slot.outline.enabled = true;
            }
            else
            {
                slot.outline.enabled = false;
            }
        }

        if (currentID == -1) return;

        var data = DataManager.Instance.RecipeDatas[currentID];
        if (data.FirstDropItemID != 0)
        {
            text1.text = $"{InventoryManager.Instance.GetItemCount(data.FirstDropItemID)} / {data.FirstDropItemCount}";
            image1.sprite = DataManager.Instance.ItemDatas[data.FirstDropItemID].Sprite;
        }

        if (data.SecondDropItemID != 0)
        {
            text2.text =
                $"{InventoryManager.Instance.GetItemCount(data.SecondDropItemID)} / {data.SecondDropItemCount}";
            image2.sprite = DataManager.Instance.ItemDatas[data.SecondDropItemID].Sprite;
        }

        if (data.ThirdDropItemID != 0)
        {
            text3.text = $"{InventoryManager.Instance.GetItemCount(data.ThirdDropItemID)} / {data.ThirdDropItemCount}";
            image3.sprite = DataManager.Instance.ItemDatas[data.ThirdDropItemID].Sprite;
        }
    }

    private void OnClickExitButton() => UIManager.Instance.CloseUI<UIKitchen>();
}
