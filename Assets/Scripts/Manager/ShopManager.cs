using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//구조를 짜고 나서의 결론:
//정말로 싱글턴으로 만들어 게임 실행 매 순간마다 이걸 계산하는게 과연 옳은 일일까요?
//그냥 단순히 흐른 시간만 계산하고, 거점으로 돌아왔을 때 한꺼번에 이 시간을 판매 아이템 리스트에 적용하는 것이 더 효율적이지 않을까요?
//고민해봐야겠습니다.
public class ShopManager : Singleton<ShopManager>
{
    //List<Item> ItemsForSale = new();
    //private bool isSelling = false;

    //private readonly List<IShopObserver> shopObservers = new();
    
    //private Coroutine sellItemCoroutine;
    //private void Start(){
    //  PlayerManager.Instance.OnShopLoaded();
    //}
    
    //private void Update(){
    //    if(!isSelling) return;
    //    if(ItemsForSale.Count == 0) return;
    //    SellItem();
    //}
    
    //public void AddItemForSale(List<Item> items){
    //    foreach(var item in items){
    //        ItemsForSale.Add(item);
    //    }
    //}

    //private void SellItem(){
    //    if(sellItemCoroutine != null) return; //이렇게 적어도 중복 검사가 될까요? 안해봐서 모르겠어요. 일단 Update에서 체크는 하고 있지만, 혹시 몰라서 적은 방어코드에요.
    //    isSelling = true;
    //    StartCoroutine(SellItemProcess());
    //}
    
    //private IEnumerator SellItemProcess(){
    //  try
    // {
    //    if(ItemsForSale.Count == 0) yield break;
    //    yield return new WaitForSeconds(ItemsForSale[0].time??); -> 아직 아이템 관련 스크립트가 없어서, 임시 이름.
    //    NotifyObservers(ItemsForSale[0].price);
    //    ItemsForSale[0].Remove();
    //    
    //    yield break;
    // }
    // finally{
    //    isSelling = false;
    //    sellItemCoroutine = null;
    //  }
    //}
    
    //옵저버 관련 코드.
    //public void AddObserver(IShopObserver observer){
    //  if(!shopObservers.Contains(observer))
    // shopObservers.Add(observer);
    // }
    
    //public void RemoveObserver(IShopObserver observer){
    //  if(shopObservers.Contains(observer))
    //  shopObservers.Remove(observer);
    // }
    
    //private void NotifyObservers(int price){
    //  foreach(var observer in shopObservers)
    //  observer.OnItemSold(price);
    // }
}