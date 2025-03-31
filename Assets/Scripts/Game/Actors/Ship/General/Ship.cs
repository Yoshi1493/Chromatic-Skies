using System;
using System.Collections;
using UnityEngine;

public abstract class Ship : Actor
{
    public ShipObject shipData;

    bool invincible;
    public bool Invincible
    {
        get => invincible;
        set
        {
            if (value != invincible)
            {
                invincible = value;
                InvincibleAction?.Invoke(value);
            }
        }
    }

    [SerializeField] new protected CircleCollider2D collider;

    public event Action<int> TakeDamageAction;
    public event Action LoseLifeAction;
    public event Action DeathAction;
    public event Action<bool> InvincibleAction;

    protected IEnumerator loseLifeCoroutine;
    protected IEnumerator invincibilityCoroutine;
    protected IEnumerator deathCoroutine;

    protected override void Awake()
    {
        base.Awake();
        InitShipData();
    }

    protected virtual void InitShipData()
    {
        SpriteRenderer.sprite = shipData.Sprite;

        //debug
        name = shipData.ShipName.value;
    }

    public virtual void TakeDamage(int damage)
    {
        TakeDamageAction?.Invoke(damage);
    }

    protected virtual IEnumerator LoseLife()
    {
        LoseLifeAction?.Invoke();
        yield return null;
    }

    protected virtual IEnumerator Die()
    {
        DeathAction?.Invoke();
        yield return null;
    }
}