using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameState : MonoBehaviour, IGameState
{
    public void EnterState()
    {
        Debug.Log("InGame State Enter");
        
        SceneLoadManager.Instance.LoadScene(SceneType.BaseCamp);
    }

    public void ExitState()
    {
        Debug.Log("InGame State Exit");
    }
}
