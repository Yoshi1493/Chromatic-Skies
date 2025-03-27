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

    public event Action<int> TakeDamageAction;
    public event Action LoseLifeAction;
    public event Action RespawnAction;
    public event Action DeathAction;

    public event Action<bool> InvincibleAction;

    #endregion

    [SerializeField] new protected CircleCollider2D collider;
    protected virtual float OriginalColliderRadius => 0.5f;
    protected virtual float InvincibleColliderRadius => 1.5f;

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
        collider.radius = OriginalColliderRadius;

        //debug
        name = shipData.ShipName.value;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, shipData.MaxHealth.Value);
        print($"{name} took {damage} damage.");

        TakeDamageAction?.Invoke(damage);

        if (damage > 0)
        {
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

    //called when boss transition to next attack pattern, and when player receives damage
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
        Invincible = true;
        yield return null;

        collider.radius = InvincibleColliderRadius;
        yield return WaitForSeconds(duration);

        Invincible = false;
        collider.radius = OriginalColliderRadius;

        invincibilityCoroutine = null;
    }

    public abstract void DisplayInvincibleShield(Vector3 spawnPos);
}