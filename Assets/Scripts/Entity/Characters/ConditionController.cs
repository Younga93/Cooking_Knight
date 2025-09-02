using UnityEngine;
using System;

public class ConditionController : MonoBehaviour
{
    [Header("Health")] 
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    
    public event Action<float> OnHealthChanged;
    
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = this.maxHealth;
        OnHealthChanged?.Invoke(currentHealth/maxHealth);
    }

    public void TakeDamage(float amount)
    {
        if (currentHealth <= 0) return;

        currentHealth = Mathf.Max(currentHealth - amount, 0);
        
        OnHealthChanged?.Invoke(currentHealth / maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        
        OnHealthChanged?.Invoke(currentHealth / maxHealth);
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} dead");
        //todo. 사망 구현하기.
    }
    
}