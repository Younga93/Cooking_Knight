using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// public class UIIntro : UIBase
// {
//     [SerializeField] private Image image;
//     [SerializeField] private TextMeshProUGUI text;
//     [SerializeField] private Button button;
//     private bool _isTextOn;
//     private float _time;
//     private bool _hasFadedIn;
//     private void Start()
//     {
//         image.canvasRenderer.SetAlpha(0.0f);
//         button.onClick.AddListener(OnClickButton);
//         FadeInImage();
//     }
//     private void FadeInImage()
//     {
//         if (_hasFadedIn) return;
//         image.CrossFadeAlpha(1, 1, false);
//         _hasFadedIn = true;
//     }
//
//     private void Update()
//     {
//         _time += Time.deltaTime;
//         text.gameObject.SetActive(_isTextOn);
//         SetTextOn();
//     }
//
//     private void SetTextOn()
//     {
//         if (_time > 1)
//         {
//             _isTextOn = !_isTextOn;
//             _time = 0;
//         }
//     }
//
//     private void OnClickButton()
//     {
//         Destroy(this.gameObject);
//     }
// }
public class UIIntro : UIBase
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button button;
    private bool _isTextOn;
    private float _time;

    private void Start()
    {
        if (!GameManager.Instance.isFirstBoot)
        {
            Destroy(this.gameObject);
        }
        image.canvasRenderer.SetAlpha(0f);
        var c = image.color; 
        c.a = 1f;
        image.color = c;

        FadeInImage();

        button.onClick.AddListener(OnClickButton);
    }

    private void FadeInImage()
    {
        image.CrossFadeAlpha(1f, 1f, false);
    }

    private void Update()
    {
        _time += Time.deltaTime;
        text.gameObject.SetActive(_isTextOn);
        SetTextOn();
    }

    private void SetTextOn()
    {
        if (_time > 1)
        {
            _isTextOn = !_isTextOn;
            _time = 0;
        }
    }

    private void OnClickButton()
    {
        GameManager.Instance.isFirstBoot = false;
        Destroy(this.gameObject);
    }
}
