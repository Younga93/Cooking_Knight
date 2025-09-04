using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawnManager : MonoBehaviour
{
    // 존재 가능한 최대 몬스터 수	10
    // 몬스터 생성 시간	5
    [Header("Stage Information")] 
    [SerializeField] private StageData _currentStageData;

    [SerializeField] private int _currentEnemyCount;
    private int _defeatedEnemyCount;
    
    private Dictionary<GameObject, IObjectPool<GameObject>> _enemyPools = new Dictionary<GameObject, IObjectPool<GameObject>>();
    // [SerializeField] private List<GameObject> _currentEnemies;
    // private float _respawnTimer;

    private Coroutine _enemySpawnCoroutine;
    
    private void Awake()
    {
        InitStage(_currentStageData);   //todo. Awake에서가 아니라 SceneLoadManager에서 Scene진입할때 InitStage 호출하기
    }

    private void Update()
    {
        if (_enemySpawnCoroutine == null && _currentEnemyCount < _currentStageData.maxEnemyCount)
        {
            _enemySpawnCoroutine = StartCoroutine(SpawnEnemyCoroutine());
        }
    }

    public void InitStage(StageData stageData)
    {
        this._currentStageData = stageData; //굳이 딥카피할 필요.. 없을듯?
        this._currentEnemyCount = 0;
        this._defeatedEnemyCount = 0;

        _enemyPools.Clear();

        //todo. 이너미 종류별로 풀 생성하기
        foreach (EnemyData enemyData in _currentStageData.enemies)
        {
            IObjectPool<GameObject> pool = new ObjectPool<GameObject>(() =>
                {
                    GameObject newEnemy = Instantiate(enemyData.prefab, this.transform);
                    return newEnemy;
                },
                (obj) =>
                {
                    obj.SetActive(true);
                    obj.transform.position = this.transform.position; //todo. random 위치 생성하기

                    Enemy enemy = obj.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.enabled = true;
                        enemy.Init(enemyData, this);
                    }
                },
                (obj) =>
                {
                    obj.SetActive(false);
                },
                (obj) =>
                {
                    Destroy(obj);
                },
                false,
                _currentStageData.maxEnemyCount,
                _currentStageData.maxEnemyCount
            );

            _enemyPools.Add(enemyData.prefab, pool);
        }
    }
    public void SpawnEnemy()  //todo. currentEnemycount 10미만일때 호출하기.
    {
        EnemyData enemyData = _currentStageData.GetRandomEnemyFromList();
        if (_enemyPools.TryGetValue(enemyData.prefab, out IObjectPool<GameObject> pool))
        {
            pool.Get();
            _currentEnemyCount++;
        }
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        yield return new WaitForSeconds(_currentStageData.respawnTime);
        SpawnEnemy();
        _enemySpawnCoroutine = null;
    }

    // private IEnumerator SpawnEnemyCoroutine(EnemyData newEnemy)
    // {
    //     yield return new WaitForSeconds(_currentStageData.respawnTime);
    //
    //     _currentEnemyCount++;
    //     Vector2 spawnPosition = new Vector2(this.transform.position.x, this.transform.position.y);
    //
    //     GameObject enemyObject = Instantiate(newEnemy.prefab, spawnPosition, Quaternion.identity);
    //     _currentEnemies.Add(enemyObject);
    //     Enemy enemy = enemyObject.GetComponent<Enemy>();
    //     if (enemy != null)
    //     {
    //         enemy.Init(newEnemy, this);
    //     }
    //
    //     _enemySpawnCoroutine = null;
    // }
    
    
    // private void SpawnEnemies() //한번에 스폰하는 건데 todo. 하나씩 스폰시키기
    // {
    //     if (_currentStageData == null)
    //     {
    //         Debug.Log("스테이지 없음");
    //         return;
    //     }
    //     Debug.Log($"현재 스테이지의 적 목록 크기: {_currentStageData.maxEnemyCount}");
    //     
    //     while(_currentEnemyCount < _currentStageData.maxEnemyCount)
    //     {
    //         _currentEnemyCount++;
    //         //todo. random position으로 변경하기
    //         Vector2 spawnPosition = new Vector2(this.transform.position.x, this.transform.position.y);
    //
    //         EnemyData enemyData = _currentStageData.GetRandomEnemyFromList();
    //         GameObject enemyObject = Instantiate(enemyData.prefab, spawnPosition, Quaternion.identity);
    //         _currentEnemies.Add(enemyObject);
    //         Enemy enemy = enemyObject.GetComponent<Enemy>();
    //         if (enemy != null)
    //         {
    //             enemy.Init(enemyData, this);
    //         }
    //     }
    // }
    
    public void OnEnemyDefeated(GameObject enemyObject)
    {
        _currentEnemyCount--;
        _defeatedEnemyCount++;

        Enemy enemyComponent = enemyObject.GetComponent<Enemy>();
        if (enemyComponent != null)
        {
            if (_enemyPools.TryGetValue(enemyComponent._enemyData.prefab, out IObjectPool<GameObject> pool))
            {
                pool.Release(enemyObject);
            }
        }

        if (_defeatedEnemyCount >= _currentStageData.totalEnemyToDefeatCount)
        {
            //todo. 스테이지 클리어
        }
    }
}
