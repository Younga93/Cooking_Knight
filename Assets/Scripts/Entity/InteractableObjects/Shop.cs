using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour, IShopLevelObserver
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite shopLevel1Sprite;
    [SerializeField] private Sprite shopLevel2Sprite;
    [SerializeField] private Sprite shopLevel3Sprite;

    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        GameManager.Instance.AddObserver(this);
        OnLevelUp();
    }
    
    private void OnDestroy()
    {
        GameManager.Instance.RemoveObserver(this);
    }

    public void OnLevelUp()
    {
        switch (GameManager.Instance.shopLevel)
        {
            case 1:
                _spriteRenderer.sprite = shopLevel1Sprite;
                break;
            case 2:
                _spriteRenderer.sprite = shopLevel2Sprite;
                break;
            case 3:
                _spriteRenderer.sprite = shopLevel3Sprite;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            UIManager.Instance.OpenUI<UIShop>();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            UIManager.Instance.CloseUI<UIShop>();
        }
    }
}
