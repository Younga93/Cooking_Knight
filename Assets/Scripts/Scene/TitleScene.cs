using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScene : SceneBase
{
    public override string GetSceneName()
    {
        return SceneNames.Title;
    }

    public override void OnLoad()
    {
        Debug.Log($"{GetSceneName()}: OnLoad");
    }

    public override void OnUnload()
    {
        Debug.Log($"{GetSceneName()}: OnUnload");
    }
}
