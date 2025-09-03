using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    // 존재 가능한 최대 몬스터 수	10
    // 몬스터 생성 시간	5
    [Header("Stage Information")] 
    [SerializeField] private StageData _currentStageData;

    [SerializeField] private int _currentEnemyCount;
    private int _defeatedEnemyCount;
    [SerializeField] private List<GameObject> _currentEnemies;
    private float _respawnTimer;

    private Coroutine _enemySpawnCoroutine;
    
    private void Awake()
    {
        InitStage(_currentStageData);   //todo. Awake에서가 아니라 SceneLoadManager에서 Scene진입할때 InitStage 호출하기
    }

    private void Update()
    {
        if (_enemySpawnCoroutine != null) return;
        if (_currentEnemyCount < _currentStageData.maxEnemyCount)
        {
            SpawnEnemy();
        }
    }

    public void InitStage(StageData stageData)
    {
        this._currentStageData = stageData; //굳이 딥카피할 필요.. 없을듯?
        this._currentEnemyCount = 0;
        this._defeatedEnemyCount = 0;
        this._currentEnemies = new List<GameObject>();
        SpawnEnemies();
    }
    public void SpawnEnemy()  //todo. currentEnemycount 10미만일때 호출하기.
    {
        if (_enemySpawnCoroutine != null)
        {
            StopCoroutine(_enemySpawnCoroutine);
        }
        //todo. 오브젝트 풀로 생성된 녀석 가져오기. 
        //todo. 지금은 그냥 임시 데이터로 리스트 제일 첫번째꺼 가져오고 있음.
        EnemyData newEnemy = _currentStageData.enemies[0]; 
        _enemySpawnCoroutine = StartCoroutine(SpawnEnemyCoroutine(newEnemy));
    }

    private IEnumerator SpawnEnemyCoroutine(EnemyData newEnemy)
    {
        yield return new WaitForSeconds(_currentStageData.respawnTime);

        _currentEnemyCount++;
        Vector2 spawnPosition = new Vector2(this.transform.position.x, this.transform.position.y);

        GameObject enemyObject = Instantiate(newEnemy.prefab, spawnPosition, Quaternion.identity);
        _currentEnemies.Add(enemyObject);
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Init(newEnemy, this);
        }

        _enemySpawnCoroutine = null;
    }
    private void SpawnEnemies() //한번에 스폰하는 건데 todo. 하나씩 스폰시키기
    {
        if (_currentStageData == null)
        {
            Debug.Log("스테이지 없음");
            return;
        }
        Debug.Log($"현재 스테이지의 적 목록 크기: {_currentStageData.maxEnemyCount}");
        
        while(_currentEnemyCount < _currentStageData.maxEnemyCount)
        {
            _currentEnemyCount++;
            //todo. random position으로 변경하기
            Vector2 spawnPosition = new Vector2(this.transform.position.x, this.transform.position.y);

            EnemyData enemyData = _currentStageData.GetRandomEnemyFromList();
            GameObject enemyObject = Instantiate(enemyData.prefab, spawnPosition, Quaternion.identity);
            _currentEnemies.Add(enemyObject);
            Enemy enemy = enemyObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Init(enemyData, this);
            }
        }
    }
    
    public void OnEnemyDefeated(GameObject enemy)
    {
        _currentEnemyCount--;
        // if (_currentEnemyCount == 0)
        // {
        //     //todo. 스테이지 클리어
        //     Debug.Log("Stage Cleared");
        //     // GameManager.Instance.EndStage();
        // }
    }
}
