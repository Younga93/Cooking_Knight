using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public void DropItem(int id, Transform position)
    {
        if (DataManager.Instance.GetDroppedItem(id, out var go))
        {
            Instantiate(go, position.position, Quaternion.identity);
        }
    }
}
