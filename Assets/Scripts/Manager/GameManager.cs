using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [field: SerializeField]
    public Player Player { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Player = FindAnyObjectByType<Player>();
    }

    public void GamePaused()
    {
        //게임 일시정지
        
    }
    public void GameOver()
    {
        //게임오버
        
    }

    public void SaveGame()
    {
        
    }
}
