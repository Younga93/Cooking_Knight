using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITrigger : MonoBehaviour
{
    void Start()
    {
        UIManager.Instance.OpenUI<UIInventory>();
        Destroy(gameObject);
    }
    
}
