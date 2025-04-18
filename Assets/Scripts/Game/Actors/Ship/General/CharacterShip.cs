using System;
using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CharacterShip : Ship
{
    [SerializeField] protected IntObject currentLives;
    [SerializeField] protected IntObject currentHealth;

    protected virtual float OriginalColliderRadius => 0.5f;
    protected virtual float InvincibleColliderRadius => 1.5f;

    public event Action RespawnAction;
    public virtual float RespawnTime => 2f;

    IEnumerator healthRefillCoroutine;
    [SerializeField] AnimationCurve healthRefillInterpolation;

    protected virtual void OnEnable()
    {
        RefillHealth();
    }

    protected override void InitShipData()
    {
        base.InitShipData();

        collider.radius = OriginalColliderRadius;

        currentLives.value = shipData.MaxLives.Value;
        currentHealth.value = 0;

        //debug
        name = shipData.ShipName.value;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        currentHealth.value -= damage;
        currentHealth.value = Mathf.Clamp(currentHealth.value, 0, shipData.MaxHealth.Value);
        print($"{name} took {damage} damage.");

        if (damage > 0)
        {
            if (currentHealth.value <= 0)
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

    protected override IEnumerator LoseLife()
    {
        currentLives.value--;
        yield return base.LoseLife();

        collider.enabled = false;

        if (currentLives.value <= 0)
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
        RefillHealth();
    }

    void RefillHealth()
    {
        if (healthRefillCoroutine != null)
        {
            StopCoroutine(healthRefillCoroutine);
        }

        healthRefillCoroutine = _RefillHealth();
        StartCoroutine(healthRefillCoroutine);
    }

    IEnumerator _RefillHealth()
    {
        float currentLerpTime = 0f;

        while (currentHealth.value < shipData.MaxHealth.Value)
        {
            float t = currentLerpTime / RespawnTime;
            currentHealth.value = (int)Mathf.Lerp(0, shipData.MaxHealth.Value, healthRefillInterpolation.Evaluate(t));

            currentLerpTime += Time.deltaTime;
            yield return null;
        }
    }

    protected override IEnumerator Die()
    {
        yield return base.Die();
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
}