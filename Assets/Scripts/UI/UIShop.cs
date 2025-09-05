using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIShop : UIBase
{
    [SerializeField] private Button healthButton;
    [SerializeField] private Button attackButton;
    [SerializeField] private Button priceButton;
    [SerializeField] private Button timeButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private TextMeshProUGUI priceText;
    private int _defaultCost = 500;
    protected override void OnOpen()
    {
        PlayerManager.Instance.player.isAttackable = false;
        priceText.color = Color.white;
        healthButton.onClick.AddListener(OnClickHealthButton);
        attackButton.onClick.AddListener(OnClickAttackButton);
        priceButton.onClick.AddListener(OnClickPriceButton);
        timeButton.onClick.AddListener(OnClickTimeButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }
    
    protected override void OnClose()
    {
        PlayerManager.Instance.player.isAttackable = true;
        healthButton.onClick.RemoveListener(OnClickHealthButton);
        attackButton.onClick.RemoveListener(OnClickAttackButton); 
        priceButton.onClick.RemoveListener(OnClickPriceButton);
        timeButton.onClick.RemoveListener(OnClickTimeButton);
        exitButton.onClick.RemoveListener(OnClickExitButton);
    }

    private void OnClickHealthButton()
    {
        AudioManager.Instance.PlayClickSoundEffect();
        if (InventoryManager.Instance.TryPurchase(_defaultCost))
        {
            PlayerManager.Instance.AddAdditionalMaxHealth(50);
            priceText.color = Color.white;
        }
        else
        {
            priceText.color = Color.red;
        }
    }

    private void OnClickAttackButton()
    {
        AudioManager.Instance.PlayClickSoundEffect();
        if (InventoryManager.Instance.TryPurchase(_defaultCost))
        {
            PlayerManager.Instance.AddAttackPower(5.0f);
            priceText.color = Color.white;
        }
        else
        {
            priceText.color = Color.red;
        }
    }

    private void OnClickPriceButton()
    {
        AudioManager.Instance.PlayClickSoundEffect();
        if (InventoryManager.Instance.TryPurchase(_defaultCost))
        {
            RestaurantManager.Instance.IncreaseRevenueMultiplier();
            priceText.color = Color.white;
            GameManager.Instance.RestaurantLevelUp();
        }
        else
        {
            priceText.color = Color.red;
        }
    }

    private void OnClickTimeButton()
    {
        AudioManager.Instance.PlayClickSoundEffect();
        if (InventoryManager.Instance.TryPurchase(_defaultCost))
        {
            KitchenManager.Instance.ReduceTimeMultiplier();
            priceText.color = Color.white;
            GameManager.Instance.KitchenLevelUp();
        }
        else
        {
            priceText.color = Color.red;
        }
    }

    private void OnClickExitButton()
    {
        AudioManager.Instance.PlayClickSoundEffect();
        UIManager.Instance.CloseUI<UIShop>();
    }
}
