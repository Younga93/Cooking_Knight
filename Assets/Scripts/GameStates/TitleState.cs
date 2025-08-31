using UnityEngine;

public class TitleState: MonoBehaviour, IGameState
{
    public void EnterState()
    {
        Debug.Log("Title State Enter");
        
        //아직 버튼 없으므로 임시로 진행
        Invoke("OnClickStartButton", 5f);
    }

    public void ExitState()
    {
        Debug.unityLogger.Log("Title State Exit");
    }

    public void OnClickStartButton()
    {
        StartManager.Instance.ChangeState(new InGameState());
    }
}
