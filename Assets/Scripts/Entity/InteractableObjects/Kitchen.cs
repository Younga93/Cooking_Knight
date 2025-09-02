using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Kitchen : MonoBehaviour
{
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
