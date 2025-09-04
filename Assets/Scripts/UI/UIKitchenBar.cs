using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKitchenBar : UIBase
{
    [SerializeField] private Image bar;

    public void Fill(float value, float max)
    {
        bar.fillAmount = value / max;
    }
}
