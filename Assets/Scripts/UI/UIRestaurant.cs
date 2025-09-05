using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRestaurant : UIBase
{
    [SerializeField] private Button exitButton;
    [SerializeField] private HorizontalLayoutGroup layoutGroup;
    private Transform _contents;
    private List<UIFoodSlot> _foodSlots = new();
    [SerializeField] private TextMeshProUGUI emptyText;

    protected override void OnOpen()
    {
        AudioManager.Instance.PlayRestaurantSoundEffect();
        exitButton.onClick.AddListener(OnClickExitButton);
        PlayerManager.Instance.player.isAttackable = false;
        if (_contents == null)
        {
            _contents = layoutGroup.GetComponent<Transform>();
        }

        if (InventoryManager.Instance.inventory != null && InventoryManager.Instance.inventory.foods.Count == 0)
        {
            emptyText.gameObject.SetActive(true);
            return;
        }

        emptyText.gameObject.SetActive(false);
        
        foreach (var data in InventoryManager.Instance.inventory.foods)
        {
            IncreaseWidth(300);
            var slot = UIManager.Instance.CreateSlotUI<UIFoodSlot>(_contents);
            slot.SetFood(data);
            _foodSlots.Add(slot);
        }
        
    }

    protected override void OnClose()
    {
        ClearSlotsAndResetWidth();
        exitButton.onClick.RemoveListener(OnClickExitButton);
        PlayerManager.Instance.player.isAttackable = true;
    }

    private void IncreaseWidth(float width)
    {
        var rt = _contents.GetComponent<RectTransform>();
        var currentWidth = rt.rect.width;
        var targetWidth = currentWidth + width;
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetWidth);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rt);
    }

    private void ClearSlotsAndResetWidth()
    {
        if (_foodSlots != null)
        {
            for (int i = 0; i < _foodSlots.Count; i++)
            {
                if (_foodSlots[i] != null)
                    Destroy(_foodSlots[i].gameObject);
            }

            _foodSlots.Clear();
        }

        var rt = _contents.GetComponent<RectTransform>();
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rt);
    }

    private void OnClickExitButton()
    {
        UIManager.Instance.CloseUI<UIRestaurant>();
        AudioManager.Instance.PlayClickSoundEffect();
    }
}