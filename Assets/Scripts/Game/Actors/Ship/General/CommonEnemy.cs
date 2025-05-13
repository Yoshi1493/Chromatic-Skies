using System.Collections;
using UnityEngine;

public class CommonEnemy : Ship
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
            LeaveScene();
        }
    }

    protected override IEnumerator Die()
    {
        yield return base.Die();

        if (currentHealth <= 0)
        {
            var collectible = CollectibleObjectPool.Instance.Get((int)CollectibleType.Score);

            collectible.transform.position = transform.position;
            collectible.gameObject.SetActive(true);
            collectible.enabled = true;
        }

        Destroy(gameObject);
    }

    //called by CommonEnemyMovement if finished leaving the scene, or when health reaches 0
    public void LeaveScene()
    {
        if (deathCoroutine != null)
        {
            StopCoroutine(deathCoroutine);
        }

        deathCoroutine = Die();
        StartCoroutine(deathCoroutine);
    }
}