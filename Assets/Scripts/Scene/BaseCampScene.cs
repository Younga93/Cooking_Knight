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
        Debug.Log($"{GetSceneName()}: OnLoad");
    }

    public override void OnUnload()
    {
        Debug.Log($"{GetSceneName()}: OnUnload");
    }
}
