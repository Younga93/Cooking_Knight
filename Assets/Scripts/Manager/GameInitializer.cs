using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private SceneType sceneType;
    private void Awake()
    {
        //생성되어야하는 싱글톤 순서대로...
        //(현재 제네릭 싱글톤은 게으른 초기화라서 제일 처음 접근할때서야 생성되므로)
        //GameManager.Instance.ToString()과 같이 접근하면 됩니다.
        
        GameManager.Instance.ToString();
        SceneLoadManager.Instance.LoadScene(sceneType);
    }
}
