using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Kitchen : MonoBehaviour, IShopLevelObserver
{
    private UIKitchenBar _timeBar;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite kitchenLevel1Sprite;
    [SerializeField] private Sprite kitchenLevel2Sprite;
    [SerializeField] private Sprite kitchenLevel3Sprite;
    private void Start()
    {
        _timeBar = UIManager.Instance.CreateBarUI<UIKitchenBar>();
        _timeBar.gameObject.SetActive(false);
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        KitchenManager.Instance.OnTimeStarted += ActivateBar;
        KitchenManager.Instance.TimeChanged += ChangeFillAmount;
        OnLevelUp();
        GameManager.Instance.AddObserver(this);
    }
    
    public void OnLevelUp()
    {
        switch (GameManager.Instance.kitchenLevel)
        {
            case 1:
                _spriteRenderer.sprite = kitchenLevel1Sprite;
                break;
            case 2:
                _spriteRenderer.sprite = kitchenLevel2Sprite;
                break;
            case 3:
                _spriteRenderer.sprite = kitchenLevel3Sprite;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void OnDestroy()
    {
        KitchenManager.Instance.OnTimeStarted -= ActivateBar;
        KitchenManager.Instance.TimeChanged -= ChangeFillAmount;
        GameManager.Instance.RemoveObserver(this);
    }

    private void ActivateBar(bool activate)
    {
        _timeBar.gameObject.SetActive(activate);
    }

    private void ChangeFillAmount(float amount, float max)
    {
        _timeBar.Fill(amount, max);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            UIManager.Instance.OpenUI<UIKitchen>();
            KitchenManager.Instance.OnEnterKitchen();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            UIManager.Instance.CloseUI<UIKitchen>();
        }
    }
}
