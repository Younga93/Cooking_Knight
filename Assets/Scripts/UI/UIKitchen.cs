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
    [SerializeField] private Button cookButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        _contents = layoutGroup.GetComponent<Transform>();
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

        currentID = -1;
        exitButton.onClick.AddListener(OnClickExitButton);
        cookButton.onClick.AddListener(OnClickCookButton);
        RefreshUI();
    }

    protected override void OnClose()
    {
        ClearSlotsAndResetHeight();
        exitButton.onClick.RemoveListener(OnClickExitButton);
        cookButton.onClick.RemoveListener(OnClickCookButton);
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

    private void ClearSlotsAndResetHeight()
    {
        if (_recipeSlots != null)
        {
            for (int i = 0; i < _recipeSlots.Count; i++)
            {
                if (_recipeSlots[i] != null)
                    Destroy(_recipeSlots[i].gameObject);
            }
            _recipeSlots.Clear();
        }

        var rt = _contents.GetComponent<RectTransform>();
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rt);
    }
    
    private void RefreshUI()
    {
        if (currentID == -1)
        {
            text1.gameObject.SetActive(false);
            image1.gameObject.SetActive(false);
            text2.gameObject.SetActive(false);
            image2.gameObject.SetActive(false);
            text3.gameObject.SetActive(false);
            image3.gameObject.SetActive(false);
            return;
        }

        foreach (var slot in _recipeSlots)
        {
            slot.outline.enabled = slot.recipeData.ID == currentID;
        }

        var data = DataManager.Instance.RecipeDatas[currentID];
        if (data.FirstDropItemID != 0)
        {
            text1.gameObject.SetActive(true);
            image1.gameObject.SetActive(true);
            text1.text = $"{InventoryManager.Instance.GetItemCount(data.FirstDropItemID)} / {data.FirstDropItemCount}";
            image1.sprite = DataManager.Instance.ItemDatas[data.FirstDropItemID].Sprite;
            text1.color = Color.white;
        }
        else
        {
            text1.gameObject.SetActive(false);
            image1.gameObject.SetActive(false);
        }

        if (data.SecondDropItemID != 0)
        {
            text2.gameObject.SetActive(true);
            image2.gameObject.SetActive(true);
            text2.text =
                $"{InventoryManager.Instance.GetItemCount(data.SecondDropItemID)} / {data.SecondDropItemCount}";
            image2.sprite = DataManager.Instance.ItemDatas[data.SecondDropItemID].Sprite;
            text1.color = Color.white;
        }
        else
        {
            text2.gameObject.SetActive(false);
            image2.gameObject.SetActive(false);
        }

        if (data.ThirdDropItemID != 0)
        {
            text3.gameObject.SetActive(true);
            image3.gameObject.SetActive(true);
            text3.text = $"{InventoryManager.Instance.GetItemCount(data.ThirdDropItemID)} / {data.ThirdDropItemCount}";
            image3.sprite = DataManager.Instance.ItemDatas[data.ThirdDropItemID].Sprite;
            text1.color = Color.white;
        }
        else
        {
            text3.gameObject.SetActive(false);
            image3.gameObject.SetActive(false);
        }
    }

    private void OnClickExitButton() => UIManager.Instance.CloseUI<UIKitchen>();

    private void OnClickCookButton()
    {
        if (KitchenManager.Instance.IsCookAvailable(currentID))
        {
            KitchenManager.Instance.Cook(currentID);
        }
        else
        {
            text1.color = Color.red;
            text2.color = Color.red;
            text3.color = Color.red;
        }
    }
}