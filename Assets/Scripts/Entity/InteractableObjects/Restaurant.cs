using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restaurant : MonoBehaviour, IShopLevelObserver
{
    private UIRestaurantBar _timeBar;
    
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite restaurantLevel1Sprite;
    [SerializeField] private Sprite restaurantLevel2Sprite;
    [SerializeField] private Sprite restaurantLevel3Sprite;
    private void Start()
    {
        _timeBar = UIManager.Instance.CreateBarUI<UIRestaurantBar>();
        _timeBar.gameObject.SetActive(false);
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        RestaurantManager.Instance.OnTimeStarted += ActivateBar;
        RestaurantManager.Instance.TimeChanged += ChangeFillAmount;
        GameManager.Instance.AddObserver(this);
        OnLevelUp();
    }

    private void OnDestroy()
    {
        GameManager.Instance.RemoveObserver(this);
    }

    public void OnLevelUp()
    {
        switch (GameManager.Instance.restaurantLevel)
        {
            case 1:
                _spriteRenderer.sprite = restaurantLevel1Sprite;
                break;
            case 2:
                _spriteRenderer.sprite = restaurantLevel2Sprite;
                break;
            case 3:
                _spriteRenderer.sprite = restaurantLevel3Sprite;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private void OnDisable()
    {
        RestaurantManager.Instance.OnTimeStarted -= ActivateBar;
        RestaurantManager.Instance.TimeChanged -= ChangeFillAmount;
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
            UIManager.Instance.OpenUI<UIRestaurant>();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            UIManager.Instance.CloseUI<UIRestaurant>();
        }
    }

}
