using UnityEngine;
using System;

public abstract class ConditionController : MonoBehaviour
{
    [Header("Health")] 
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currentHealth;
    
    public event Action<float> OnHealthChanged;
    
    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth/maxHealth);
    }

    public virtual void TakeDamage(float amount)
    {
        Debug.Log($"{gameObject.name}TakeDamage");
        if (currentHealth <= 0 || amount == 0) return;

        currentHealth = Mathf.Max(currentHealth - amount, 0);
        
        OnHealthChanged?.Invoke(currentHealth / maxHealth);

        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        
        OnHealthChanged?.Invoke(currentHealth / maxHealth);
    }

    protected abstract void OnDeath();
}