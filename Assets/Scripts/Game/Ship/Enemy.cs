using UnityEngine;

public class Enemy : Ship
{
    EnemyBulletSystem[] enemyBulletSystems;

    protected override void Awake()
    {
        base.Awake();

        enemyBulletSystems = GetComponentsInChildren<EnemyBulletSystem>(true);
    }

    protected override void LoseLife()
    {
        int currentSystem = shipData.Lives.OriginalValue - shipData.Lives.CurrentValue;

        base.LoseLife();

        if (shipData.Lives.CurrentValue > 0)
        {
            enemyBulletSystems[currentSystem].enabled = false;
            enemyBulletSystems[currentSystem + 1].enabled = true;
        }
    }

    protected override void Die()
    {
        gameObject.SetActive(false);
    }
}