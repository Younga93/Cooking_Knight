using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        GameManager.Instance.isFirstBoot = false;
        image.canvasRenderer.SetAlpha(0f);
        var c = image.color; 
        c.a = 1f;
        image.color = c;

        FadeInImage();

        button.onClick.AddListener(OnClickButton);
        PlayerManager.Instance.player.isMovable = false;
        PlayerManager.Instance.player.isAttackable = false;
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
        PlayerManager.Instance.player.isMovable = true;
        PlayerManager.Instance.player.isAttackable = true;
        AudioManager.Instance.PlayClickSoundEffect();
        Destroy(this.gameObject);
    }
}
