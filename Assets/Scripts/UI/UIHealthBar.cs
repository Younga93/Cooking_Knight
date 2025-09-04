using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIHealthBar : UIBase
{
    [SerializeField] private Image healthBar;

    protected override void OnOpen()
    {
        PlayerManager.Instance.player.ConditionController.OnHealthChanged += Fill;
    }

    protected override void OnClose()
    {
        PlayerManager.Instance.player.ConditionController.OnHealthChanged -= Fill;   
    }

    private void Fill(float value)
    {
        healthBar.fillAmount = value;
    }
}
