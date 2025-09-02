using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [field: SerializeField]
    public Player Player { get; private set; }
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
