using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCampScene : SceneBase
{
    public override string GetSceneName()
    {
        return SceneNames.BaseCamp;
    }

    public override void OnLoad()
    {
        if (!DataManager.Instance.isLoaded)
        {
            DataManager.Instance.LoadData();
            AudioManager.Instance.PlayBGM();
        }

        if (GameManager.Instance.isFirstBoot)
        {
            UIManager.Instance.OpenUI<UIIntro>();
        }
        else
        {
            UIManager.Instance.CreateUIDontDestroy<UIFadeOut>();            
        }
        UIManager.Instance.OpenUI<UISettingsButton>();
        UIManager.Instance.OpenUI<UIInventory>();
        Debug.Log($"{GetSceneName()}: OnLoad");
    }

    public override void OnUnload()
    {
        Debug.Log($"{GetSceneName()}: OnUnload");
    }
}
