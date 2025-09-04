using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRestaurantBar : UIBase
{
    [SerializeField] private Image bar;
    
    public void Fill(float value, float max)
    {
        bar.fillAmount = value / max;
    }
}
