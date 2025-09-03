using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    private readonly int _collectedHash = Animator.StringToHash("Collected");
    public ItemData itemData;
    private Animator _animator;
    private bool _isCollected;
    private void Start()
    {
        _isCollected = false;
        _animator = GetComponent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IItemCollector>(out var collector))
        {//세상에나, IItemCollector가 필요없을 수도 있다. 단순히 Inventory 컴포넌트를 가져와서 넣어볼 수도 있겠다!
            if (!_isCollected)
            {
                _isCollected = true;
                collector.AddItem(itemData);
                _animator.SetTrigger(_collectedHash);
                Destroy(gameObject, 1.0f);
            }
        }
    }
}