using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    private Dictionary<SceneType, SceneBase> _scenes = new Dictionary<SceneType, SceneBase>();
    private SceneBase _previousScene;
    private SceneBase _currentScene;
    public event Action OnSceneChanged;
    protected override void Awake() 
    {
        base.Awake();
        _scenes.Add(SceneType.Intro, new IntroScene());
        _scenes.Add(SceneType.Title, new TitleScene());
        _scenes.Add(SceneType.BaseCamp, new BaseCampScene());
        _scenes.Add(SceneType.Stage, new StageScene());
    }

    public void LoadScene(SceneType sceneType)
    {
        if(_currentScene == _scenes[sceneType]) return;
        
        StartCoroutine(LoadSceneProcess(sceneType));
    }

    private IEnumerator LoadSceneProcess(SceneType sceneType)
    {
        //이전씬 존재하면 씬 정리 작업하기.
        OnSceneChanged?.Invoke();
        _currentScene?.OnUnload();
        
        _previousScene = _currentScene;
        _currentScene = _scenes[sceneType];
        
        //실제 다음 씬 비동기 로드
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_currentScene.GetSceneName());
        asyncOperation.allowSceneActivation = false;    //로딩 완료 후에도 활성화 false
        
        while (asyncOperation.progress < 0.9f)
        {
            //todo. 로딩 진행률 표시할 때 여기에서. 근데 필요한가..? 일단 넣어놓고 기획팀에게 연출 물어보기.
            yield return null;
        }
        
        asyncOperation.allowSceneActivation = true;
        yield return new WaitForEndOfFrame();   //렌더링 끝날때까지 대기
        _currentScene.OnLoad(); //씬 초기화 진행
    }
}
