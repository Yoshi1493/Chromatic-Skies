using System.Collections;
using UnityEngine;

public class Enemy : Ship
{
    int currentHealth;

    protected override void InitShipData()
    {
        base.InitShipData();
        currentHealth = shipData.MaxHealth.Value;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, shipData.MaxHealth.Value);
        print($"{name} took {damage} damage.");

        if (currentHealth <= 0)
        {
            if (deathCoroutine != null)
            {
                StopCoroutine(deathCoroutine);
            }

            deathCoroutine = Die();
            StartCoroutine(deathCoroutine);
        }
    }

    protected override IEnumerator Die()
    {
        yield return base.Die();

        collider.enabled = false;
        SpriteRenderer.enabled = false;
        gameObject.SetActive(false);
    }
}