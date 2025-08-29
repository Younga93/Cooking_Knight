using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    private Dictionary<SceneType, SceneBase> scenes = new Dictionary<SceneType, SceneBase>();
    private SceneBase previousScene;
    private SceneBase currentScene;

    private void Awake()
    {
        //Dictionary scenes 매핑
    }

    public void LoadScene(SceneType sceneType)
    {
        
    }

    private IEnumerator LoadSceneProcess(SceneType sceneType)
    {
        yield return null;
    }
}
