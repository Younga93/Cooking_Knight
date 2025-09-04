using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIHealthBar : UIBase
{
    [SerializeField] private Image healthBar;

    protected override void OnOpen()
    {
        GameManager.Instance.Player.ConditionController.OnHealthChanged += Fill;
    }

    protected override void OnClose()
    {
        GameManager.Instance.Player.ConditionController.OnHealthChanged -= Fill;   
    }

    private void Fill(float value)
    {
        healthBar.fillAmount = value;
    }
}
