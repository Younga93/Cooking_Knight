using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Kitchen : MonoBehaviour
{
    private UIKitchenBar _timeBar;

    private void Start()
    {
        _timeBar = UIManager.Instance.CreateBarUI<UIKitchenBar>();
        _timeBar.gameObject.SetActive(false);

        KitchenManager.Instance.OnTimeStarted += ActivateBar;
        KitchenManager.Instance.TimeChanged += ChangeFillAmount;
    }

    private void OnDestroy()
    {
        KitchenManager.Instance.OnTimeStarted -= ActivateBar;
        KitchenManager.Instance.TimeChanged -= ChangeFillAmount;
    }

    private void ActivateBar(bool activate)
    {
        _timeBar.gameObject.SetActive(activate);
    }

    private void ChangeFillAmount(float amount, float max)
    {
        _timeBar.Fill(amount, max);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            UIManager.Instance.OpenUI<UIKitchen>();
            KitchenManager.Instance.OnEnterKitchen();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            UIManager.Instance.CloseUI<UIKitchen>();
        }
    }
}
