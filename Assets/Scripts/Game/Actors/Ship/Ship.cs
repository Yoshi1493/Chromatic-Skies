using System;
using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public abstract class Ship : Actor
{
    #region Scriptable Object properties

    public ShipObject shipData;

    [HideInInspector] public int currentLives;
    [HideInInspector] public int currentHealth;

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

    public virtual float RespawnTime => 2f;

    #endregion

    #region Actions

    public event Action TakeDamageAction;
    public event Action LoseLifeAction;
    public event Action RespawnAction;
    public event Action DeathAction;

    public event Action<bool> InvincibleAction;

    #endregion

    new protected Collider2D collider;

    IEnumerator loseLifeCoroutine;
    IEnumerator invincibilityCoroutine;
    IEnumerator deathCoroutine;

    protected override void Awake()
    {
        base.Awake();
        InitShipData();
    }

    void InitShipData()
    {
        //appearance
        SpriteRenderer.sprite = shipData.Sprite;

        //stats
        currentLives = shipData.MaxLives.Value;
        currentHealth = shipData.MaxHealth.Value;

        //collision
        collider = GetComponent<Collider2D>();

        //debug
        name = shipData.ShipName.value;
    }

    //to-do: take shipData.Defense into account for damage calculations
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //print($"{name} took {damage} damage.");

        TakeDamageAction?.Invoke();

        //check if LoseLife methods should be called
        if (currentHealth <= 0)
        {
            if (loseLifeCoroutine != null)
            {
                StopCoroutine(loseLifeCoroutine);
            }

            loseLifeCoroutine = LoseLife();
            StartCoroutine(loseLifeCoroutine);
        }
    }

    protected virtual IEnumerator LoseLife()
    {
        currentLives--;
        LoseLifeAction?.Invoke();

        collider.enabled = false;

        if (currentLives <= 0)
        {
            if (deathCoroutine != null)
            {
                StopCoroutine(deathCoroutine);
            }

            deathCoroutine = Die();
            StartCoroutine(deathCoroutine);
        }
        //only perform if ship still has lives
        else
        {
            SetInvincible(RespawnTime + 2f);
            yield return WaitForSeconds(RespawnTime);

            Respawn();
            collider.enabled = true;
        }
    }

    protected void Respawn()
    {
        RespawnAction?.Invoke();
        currentHealth = shipData.MaxHealth.Value;
    }

    protected virtual IEnumerator Die()
    {
        DeathAction?.Invoke();

        yield return WaitForSeconds(1.5f);

        SpriteRenderer.enabled = false;
    }

    //called when enemy transition to next attack pattern, and when player receives damage
    public void SetInvincible(float duration)
    {
        if (invincibilityCoroutine != null)
        {
            StopCoroutine(invincibilityCoroutine);
        }

        invincibilityCoroutine = ToggleInvincibility(duration);
        StartCoroutine(invincibilityCoroutine);
    }

    IEnumerator ToggleInvincibility(float duration)
    {
        yield return null;
        Invincible = true;
        yield return WaitForSeconds(duration);

        Invincible = false;
        invincibilityCoroutine = null;
    }
}