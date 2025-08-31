using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene : SceneBase
{
    public override string GetSceneName()
    {
        return SceneNames.Intro;
    }

    public override void OnLoad()
    {
        Debug.Log($"{GetSceneName()}: OnLoad");
        //todo. 인트로에서 할 것 한 뒤에 다음 씬으로 이동. 현재는 3초 뒤로 간단하게 함.
        SceneLoadManager.Instance.StartCoroutine(LoadNextSceneAfterDelay(3f));
    }

    public override void OnUnload()
    {
        Debug.Log($"{GetSceneName()}: OnUnload");
    }
    
    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        for (int i = (int)delay; i > 0; i--)
        {   //아직 UI 없어서 디버그로그로.
            Debug.Log($"Intro Countdown: {i}");
            yield return new WaitForSeconds(1f);
        };
        
        Debug.Log("3초 대기 완료. 타이틀 씬으로 전환합니다.");
        SceneLoadManager.Instance.LoadScene(SceneType.Title);
    }
}
