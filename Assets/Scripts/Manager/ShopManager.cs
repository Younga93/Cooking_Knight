using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//구조를 짜고 나서의 결론:
//정말로 싱글턴으로 만들어 게임 실행 매 순간마다 이걸 계산하는게 과연 옳은 일일까요?
//그냥 단순히 흐른 시간만 계산하고, 거점으로 돌아왔을 때 한꺼번에 이 시간을 판매 아이템 리스트에 적용하는 것이 더 효율적이지 않을까요?
//고민해봐야겠습니다.
public class ShopManager : Singleton<ShopManager>
{
    private List<FoodSlot> ItemsForSale = new();
    private bool isSelling = false;

    private readonly List<IShopObserver> shopObservers = new();

    private Coroutine sellItemCoroutine;

    private void Update()
    {
        if (isSelling) return;
        if (ItemsForSale.Count == 0) return;
        SellItem();
    }

    public void AddItemForSale(List<FoodSlot> items)
    {
        foreach (var item in items)
        {
            ItemsForSale.Add(item);
        }
    }
    public void AddItemForSale(FoodSlot item)
    {
        ItemsForSale.Add(item);
    }

    private void SellItem()
    {
        if (sellItemCoroutine != null) return;
        isSelling = true;
        sellItemCoroutine = StartCoroutine(SellItemProcess());
    }

    private IEnumerator SellItemProcess()
    {
        try
        {
            if (ItemsForSale.Count == 0) yield break;
            yield return new WaitForSeconds(ItemsForSale[0].foodData.SellTime);
            NotifyObservers(ItemsForSale[0].foodData.Price);
            ItemsForSale.RemoveAt(0);
        }
        finally
        {
            isSelling = false;
            sellItemCoroutine = null;
        }
    }

    public void AddObserver(IShopObserver observer)
    {
        if (!shopObservers.Contains(observer))
            shopObservers.Add(observer);
    }

    public void RemoveObserver(IShopObserver observer)
    {
        if (shopObservers.Contains(observer))
            shopObservers.Remove(observer);
    }

    private void NotifyObservers(int price)
    {
         foreach (var observer in shopObservers)
             observer.OnItemSold(price);
    }
    
}