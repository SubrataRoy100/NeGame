using System;
using UnityEngine;
public class HealthSystem
{

    private int currentHealth;
    private int maxHealth;

    public event EventHandler OnHealthChange;
    public event EventHandler OnDie;


    public HealthSystem(int maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
    }


    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        OnHealthChange?.Invoke(this, EventArgs.Empty);
        if (IsDead())
        {

            OnDie?.Invoke(this, EventArgs.Empty);
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChange?.Invoke(this, EventArgs.Empty);
    }

    public bool IsDead()
    {
        if (currentHealth <= 0)
        {

            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetHealth(int value)
    {
        currentHealth = value;
    }
}