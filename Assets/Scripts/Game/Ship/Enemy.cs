using System.Collections.Generic;
using UnityEngine;

public class Enemy : Ship
{
    [SerializeField] List<EnemyBullet> enemyBullets = new List<EnemyBullet>();

    void Start()
    {
        EnemyBulletPool.Instance.UpdatePoolableObjects(enemyBullets);
    }

    protected override void LoseLife()
    {
        int currentSystem = shipData.MaxLives.Value - shipData.CurrentLives.Value;

        base.LoseLife();

        if (shipData.CurrentLives.Value > 0)
        {
            var currentEnemyShooters = transform.GetChild(currentSystem).GetComponentsInChildren<EnemyShooter>();
            var nextEnemyShooters = transform.GetChild(currentSystem + 1).GetComponents<EnemyShooter>();

            for (int i = 0; i < currentEnemyShooters.Length; i++)
            {
                currentEnemyShooters[i].enabled = false;
            }

            for (int i = 0; i < nextEnemyShooters.Length; i++)
            {
                nextEnemyShooters[i].enabled = true;
            }
        }
    }

    protected override void Die()
    {
        gameObject.SetActive(false);
    }
}