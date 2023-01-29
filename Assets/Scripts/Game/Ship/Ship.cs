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

    [HideInInspector] public bool invincible;
    public const float RespawnTime = 1f;

    #endregion

    #region Actions

    public event Action TakeDamageAction;
    public event Action LoseLifeAction;
    public event Action RespawnAction;
    public event Action DeathAction;

    #endregion

    new protected Collider2D collider;

    IEnumerator loseLifeCoroutine;
    IEnumerator invincibilityCoroutine;

    protected override void Awake()
    {
        base.Awake();

        InitShipData();
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
            DeathAction?.Invoke();
        }
        //only perform if ship still has lives
        else
        {
            SetInvincible(RespawnTime);
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

    protected abstract void Die();

    //called when enemy transition to next attack pattern, and when player receives damage
    protected void SetInvincible(float duration)
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
        invincible = true;
        yield return WaitForSeconds(duration);
        invincible = false;
    }
}