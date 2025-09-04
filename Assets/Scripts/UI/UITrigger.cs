using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITrigger : MonoBehaviour
{
    void Start()
    {
        Debug.Log(GameManager.Instance == null);
        if (GameManager.Instance.isFirstBoot)
        {
            UIManager.Instance.OpenUI<UIIntro>();
        }
        UIManager.Instance.OpenUI<UIInventory>();
        Destroy(gameObject);
    }
    
}
