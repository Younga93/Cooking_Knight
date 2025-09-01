using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopObserver
{
    public void OnItemSold(int money);
}
