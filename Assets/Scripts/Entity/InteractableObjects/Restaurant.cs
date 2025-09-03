using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restaurant : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            //UIManager.Instance.OpenUI<UIKitchen>();
            //KitchenManager.Instance.OnEnterKitchen();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            //UIManager.Instance.CloseUI<UIKitchen>();
        }
    }

}
