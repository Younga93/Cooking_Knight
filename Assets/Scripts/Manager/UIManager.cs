using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;

public class UIManager : Singleton<UIManager>
{
    private Transform _canvas;
    private EventSystem _eventSystem;
    
    private Dictionary<string, UIBase> _uiDictionary = new();

    private void OnEnable()
    {
        SceneLoadManager.Instance.OnSceneChanged += ClearUI;
    }

    private void OnDisable()
    {
        if (SceneLoadManager.Instance == null) return;
        SceneLoadManager.Instance.OnSceneChanged -= ClearUI;
    }
    
    public void OpenUI<T>() where T : UIBase
    {
        var ui = GetUI<T>();
        ui?.OpenUI();
    }
    
    public void CloseUI<T>() where T : UIBase
    {
        //UI가 있는지 확인 먼저, 없으면 무시하기.
        if (IsExistUI<T>())
        {
            var ui = GetUI<T>();
            ui?.CloseUI();
        }
    }
    
    private T GetUI<T>() where T : UIBase
    {
        //IsExistUI()를 통해 확인하고, 있으면 _uiDictionary에 있는 친구를 뱉고, 없으면 UI를 새로 만듦.
        var uiName = typeof(T).Name;
        UIBase ui;
        ui = IsExistUI<T>() ? _uiDictionary[uiName] : CreateUI<T>(); 
        return ui as T;
    }
    
    private bool IsExistUI<T>() where T : UIBase
    {
        var uiName = typeof(T).Name;
        return _uiDictionary.TryGetValue(uiName, out var ui) && ui != null;
    }
    
    private T CreateUI<T>() where T : UIBase
    {
        //1. _uiDictionary에 해당 UI 있는지 확인, 있으면 파괴 후 사전에서 삭제
        var uiName = typeof(T).Name;
        if(_uiDictionary.TryGetValue(uiName, out var ui) && ui != null)
        {
            Destroy(ui.gameObject);
            _uiDictionary.Remove(uiName);
        }
        //2. 캔버스, 이벤트 시스템 확인
        CheckCanvas();
        CheckEventSystem();
        
        // 3. 프리팹 로드 & 생성, 이후 프리팹이 생성되었는지 확인.
        var path = Constants.UIElementsPath + uiName;
        GameObject go = ResourceManager.Instance.Create<GameObject>(path, _canvas);
        if (go == null)
        {
            Debug.LogError($"Prefab not found: {uiName}");
            return null;
        }
        // 4. 컴포넌트 획득 이후 컴포넌트가 들어갔는지 확인. 들어가지 않았다면 파괴
        T uiComponent = go.GetComponent<T>();
        if (uiComponent == null)
        {
            Debug.LogError($"Component not found: {uiName}");
            Destroy(go);
            return null;
        }
        
        // 5. Dictionary 등록
        //_uiDictionary.Add(uiName, uiComponent); <- 중복된 값이 들어갈 수 있어 위험함
        _uiDictionary[uiName] = uiComponent;
        return uiComponent;
    }

    public T CreateSlotUI<T>(Transform parent = null) where T : UIBase
    {
        string uiName = typeof(T).Name;
        string path = Constants.UIElementsPath + uiName;
        
        GameObject go = ResourceManager.Instance.Create<GameObject>(path, parent);
        if (go == null)
        {
            Debug.LogError($"Prefab not found: {uiName}");
            return null;
        }
        
        T uiComponent = go.GetComponent<T>();
        if (uiComponent == null)
        {
            Debug.LogError($"Component not found: {uiName}");
            Destroy(go);
            return null;
        }
        
        _uiDictionary[uiName] = uiComponent;
        return uiComponent;
    }

    public T CreateBarUI<T>() where T : UIBase
    {
        string uiName = typeof(T).Name;
        string path = Constants.UIElementsPath + uiName;
        
        CheckCanvas();
        CheckEventSystem();
        
        GameObject go = ResourceManager.Instance.Create<GameObject>(path, _canvas);
        if (go == null)
        {
            Debug.LogError($"Prefab not found: {uiName}");
            return null;
        }
        
        T uiComponent = go.GetComponent<T>();
        if (uiComponent == null)
        {
            Debug.LogError($"Component not found: {uiName}");
            Destroy(go);
            return null;
        }
        
        _uiDictionary[uiName] = uiComponent;
        return uiComponent;
    }
    private void CheckCanvas()
    {
        //캔버스 있는지 확인, 있으면 return
        if (_canvas != null) return;
        //없으면 경로 만들고 생성 후 _canvas 초기화
        var path = Constants.UICommonPath + Constants.Canvas;
        _canvas = ResourceManager.Instance.Create<Transform>(path, null);
    }

    private void CheckEventSystem()
    {
        //이벤트 시스템 있는지 확인, 있으면 return
        if(_eventSystem != null) return;
        //없으면 경로 만들고 생성 후 _eventSystem 초기화
        var path = Constants.UICommonPath + Constants.EventSystem;
        _eventSystem = ResourceManager.Instance.Create<EventSystem>(path, null);
    }

    private void ClearUI()
    {
        foreach (var ui in _uiDictionary.Values)
        {
            if (ui != null)
            {
                ui.CloseUI();
                Destroy(ui.gameObject);
            }
        }
        _uiDictionary.Clear();
    }
}