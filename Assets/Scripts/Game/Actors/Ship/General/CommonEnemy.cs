using System.Collections;
using UnityEngine;

public class CommonEnemy : Ship
{
    int currentHealth;
    public float HealthPercent => (float)currentHealth / shipData.MaxHealth.Value;

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
            Destroy();
        }
    }

    protected override IEnumerator Die()
    {
        yield return base.Die();
        Destroy(gameObject);
    }

    //called when health reaches 0, *or* by CommonEnemyMovement if finished leaving the scene
    public void Destroy()
    {
        if (deathCoroutine != null)
        {
            StopCoroutine(deathCoroutine);
        }

        deathCoroutine = Die();
        StartCoroutine(deathCoroutine);
    }
}