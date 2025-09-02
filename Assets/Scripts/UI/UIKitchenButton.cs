using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIKitchenButton : UIBase
{
    [SerializeField] private Button button; //

    private void OnEnable()
    {
        button?.onClick.AddListener(OnClickButton);
    }
    
    private void OnDisable()
    {
        button?.onClick.RemoveListener(OnClickButton);
    }

    void OnClickButton() => UIManager.Instance.OpenUI<UIKitchen>();

}
