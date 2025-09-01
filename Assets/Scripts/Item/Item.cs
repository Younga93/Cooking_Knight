using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private readonly int _collectedHash = Animator.StringToHash("Collected");
    public ItemData itemData;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IItemCollector>(out var collector))
        {
            collector.AddItem(itemData);
            _animator.SetTrigger(_collectedHash);
            Destroy(gameObject, 0.5f);
        }
    }
}
