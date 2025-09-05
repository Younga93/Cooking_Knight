using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UISettingsButton : UIBase
{
    [SerializeField] private Button button; //

    protected override void OnOpen()
    {
        button.onClick.AddListener(OnClickButton);
    }

    protected override void OnClose()
    {
        button.onClick.RemoveListener(OnClickButton);
    }

    private void OnClickButton()
    {
        AudioManager.Instance.PlayClickSoundEffect();
        UIManager.Instance.OpenUI<UISettings>();
    }
}
