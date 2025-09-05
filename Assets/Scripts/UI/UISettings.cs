using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UISettings : UIBase
{
    [SerializeField] private Button button; //
    [SerializeField] private Slider slider;
    protected override void OnOpen()
    {
        GameManager.Instance.GamePaused();
        RectTransform rt = this.transform as RectTransform;
        rt.SetAsLastSibling();
        slider.onValueChanged.AddListener(AudioManager.Instance.SetVolume);
        button.onClick.AddListener(OnClickButton);
        PlayerManager.Instance.player.isAttackable = false;
        PlayerManager.Instance.player.isMovable = false;
    }

    protected override void OnClose()
    {
        GameManager.Instance.GameResume();
        slider.onValueChanged.RemoveListener(AudioManager.Instance.SetVolume);
        button.onClick.RemoveListener(OnClickButton);
        PlayerManager.Instance.player.isAttackable = true;
        PlayerManager.Instance.player.isMovable = true;
    }
    
    private void OnClickButton()
    {
        AudioManager.Instance.PlayClickSoundEffect();
        UIManager.Instance.CloseUI<UISettings>();
    }
    
}
