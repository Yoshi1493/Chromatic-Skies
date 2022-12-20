using System;
using UnityEngine;

public abstract class Ship : Actor
{
    #region Scriptable Object properties

    public ShipObject shipData;

    [HideInInspector] public int currentLives;
    [HideInInspector] public int currentHealth;

    [HideInInspector] public bool invincible;
    public const int RespawnTime = 1000;     //amount of time (msec.) to wait before resuming ship functions

    #endregion

    #region Actions

    public event Action TakeDamageAction;
    public event Action LoseLifeAction;
    public event Action DeathAction;

    public delegate void RespawnDelegate();
    public RespawnDelegate RespawnAction;

    #endregion

    new protected Collider2D collider;

    protected override void Awake()
    {
        base.Awake();

        InitShipData();

        LoseLifeAction += LoseLife;
        DeathAction += Die;
    }

    void InitShipData()
    {
        //appearance
        spriteRenderer.sprite = shipData.Sprite;

        //stats
        currentLives = shipData.MaxLives.Value;
        currentHealth = shipData.MaxHealth.Value;

        //collision
        collider = GetComponent<Collider2D>();

        //debug
        name = shipData.ShipName.value;
    }

    //to-do: take shipData.Defense into account for damage calculations
    public void TakeDamage(int power)
    {
        currentHealth -= power;
        //print($"{name} took {power} damage.");

        if (currentHealth <= 0)
        {
            LoseLifeAction?.Invoke();
        }
        else
        {
            TakeDamageAction?.Invoke();
        }
    }

    protected virtual void LoseLife()
    {
        collider.enabled = false;

        currentLives--;

        if (currentLives <= 0)
        {
            DeathAction?.Invoke();
        }
        else
        {
            currentHealth = shipData.MaxHealth.Value;
            invincible = true;
        }
    }

    //called by child classes after RespawnTime has passed
    protected void Respawn()
    {
        RespawnAction?.Invoke();
    }

    protected abstract void Die();
}