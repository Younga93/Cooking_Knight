using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//구조를 짜고 나서의 결론:
//정말로 싱글턴으로 만들어 게임 실행 매 순간마다 이걸 계산하는게 과연 옳은 일일까요?
//그냥 단순히 흐른 시간만 계산하고, 거점으로 돌아왔을 때 한꺼번에 이 시간을 판매 아이템 리스트에 적용하는 것이 더 효율적이지 않을까요?
//고민해봐야겠습니다.
public class RestaurantManager : Singleton<RestaurantManager>
{
    private readonly List<FoodSlot> _itemsForSale = new();
    private bool _isSelling = false;

    private readonly List<IShopObserver> _shopObservers = new();

    private Coroutine _sellItemCoroutine;
    private float _elapsedTime;
    private bool _timeStarted;
    public event Action<float, float> TimeChanged;
    public event Action<bool> OnTimeStarted;
    public float revenueMultiplier = 1.0f;
    private void Update()
    {
        if (_timeStarted)
        {
            _elapsedTime += Time.deltaTime;
            TimeChanged?.Invoke(_elapsedTime, _itemsForSale[0].foodData.SellTime);
        }
        if (_isSelling) return;
        if (_itemsForSale.Count == 0) return;
        SellItem();
    }

    public void IncreaseRevenueMultiplier()
    {
        revenueMultiplier *= 1.1f;
    }

    public void AddItemForSale(List<FoodSlot> items)
    {
        foreach (var item in items)
        {
            _itemsForSale.Add(item);
        }
    }
    public void AddItemForSale(FoodSlot item)
    {
        _itemsForSale.Add(item);
    }

    private void SellItem()
    {
        if (_sellItemCoroutine != null) return;
        _isSelling = true;
        _sellItemCoroutine = StartCoroutine(SellItemProcess());
    }

    private IEnumerator SellItemProcess()
    {
        try
        {
            if (_itemsForSale.Count == 0) yield break;
            _timeStarted = true;
            OnTimeStarted?.Invoke(true);
            yield return new WaitForSeconds(_itemsForSale[0].foodData.SellTime);
            NotifyObservers(_itemsForSale[0].foodData.Price);
            _itemsForSale.RemoveAt(0);
        }
        finally
        {
            _elapsedTime = 0;
            _isSelling = false;
            _timeStarted = false;
            OnTimeStarted?.Invoke(false);
            _sellItemCoroutine = null;
        }
    }

    public void AddObserver(IShopObserver observer)
    {
        if (!_shopObservers.Contains(observer))
            _shopObservers.Add(observer);
    }

    public void RemoveObserver(IShopObserver observer)
    {
        if (_shopObservers.Contains(observer))
            _shopObservers.Remove(observer);
    }

    private void NotifyObservers(int price)
    {
         foreach (var observer in _shopObservers)
             observer.OnItemSold((int)(price * revenueMultiplier));
    }
    
}