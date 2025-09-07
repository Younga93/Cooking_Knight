using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimeBar : UIBase
{
    [SerializeField] private Image bar;

    public void SetTransform(Transform pos, Vector3 offset)
    {
        this.transform.position = Camera.main.WorldToScreenPoint(pos.position + offset);
    }
    public void Fill(float value, float max)
    {
        if (bar == null) return;
        bar.fillAmount = value / max;
    }
}