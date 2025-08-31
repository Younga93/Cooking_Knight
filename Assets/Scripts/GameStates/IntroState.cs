using System.Collections;
using UnityEngine;

public class IntroState: MonoBehaviour, IGameState
{
    public void EnterState()
    {
        Debug.Log("Intro State Enter");
        //todo. 인트로에서 할것들 진행할 것들
        //
        StartCoroutine(IntroSequence());
    }

    private IEnumerator IntroSequence()
    {
        //todo 인트로 때 필요한 것들 진행
        //현재는 그냥 3초 대기 후 시작함
        yield return new WaitForSeconds(3f);
        
        StartManager.Instance.ChangeState(new TitleState());
    }

    public void ExitState()
    {
        Debug.unityLogger.Log("Intro State Exit");
    }
}
