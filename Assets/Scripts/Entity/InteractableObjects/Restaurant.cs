using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restaurant : MonoBehaviour
{
    private UIRestaurantBar _timeBar;
    private void Start()
    {
        _timeBar = UIManager.Instance.CreateBarUI<UIRestaurantBar>();
        _timeBar.gameObject.SetActive(false);
        
        RestuarantManager.Instance.OnTimeStarted += ActivateBar;
        RestuarantManager.Instance.TimeChanged += ChangeFillAmount;
    }
    
    private void OnDisable()
    {
        RestuarantManager.Instance.OnTimeStarted -= ActivateBar;
        RestuarantManager.Instance.TimeChanged -= ChangeFillAmount;
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
            UIManager.Instance.OpenUI<UIRestaurant>();
            KitchenManager.Instance.OnEnterKitchen();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            UIManager.Instance.CloseUI<UIRestaurant>();
        }
    }

}
