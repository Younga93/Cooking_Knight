using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScene : SceneBase
{
    public override string GetSceneName()
    {
        return SceneNames.Stage;
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
