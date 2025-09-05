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
        UIManager.Instance.CreateUIDontDestroy<UIFadeOut>();
        UIManager.Instance.OpenUI<UIInventory>();
        Debug.Log($"{GetSceneName()}: OnLoad");

        // StageManager.Instance.InitStage(DataManager.Instance.StageDatas[0]);
        //todo. enemydata, stagedata load
        // ItemManager.Instance.
        // json으로 관리하는 걸로 바꿔야댐.
        
    }

    public override void OnUnload()
    {
        Debug.Log($"{GetSceneName()}: OnUnload");
    }
}
