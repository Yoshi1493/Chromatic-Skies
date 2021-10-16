using System.Collections.Generic;
using UnityEngine;

public class Enemy : Ship
{
    [SerializeField] List<EnemyBullet> enemyBullets = new List<EnemyBullet>();
    [SerializeField] List<Laser> enemyLasers = new List<Laser>();

    void Start()
    {
        EnemyBulletPool.Instance.UpdatePoolableObjects(enemyBullets);
        EnemyLaserPool.Instance.UpdatePoolableObjects(enemyLasers);
    }

    protected override void LoseLife()
    {
        int currentProjectileSystem = shipData.MaxLives.Value - shipData.CurrentLives.Value;

        base.LoseLife();

        if (shipData.CurrentLives.Value > 0)
        {
            var currentEnemyShooters = transform.GetChild(currentProjectileSystem).GetComponentsInChildren<INamedAttack>();
            var nextEnemyShooters = transform.GetChild(currentProjectileSystem + 1).GetComponents<INamedAttack>();

            for (int i = 0; i < currentEnemyShooters.Length; i++)
            {
                currentEnemyShooters[i].SetEnabled(false);
            }

            for (int i = 0; i < nextEnemyShooters.Length; i++)
            {
                nextEnemyShooters[i].SetEnabled(true);
            }
        }
    }

    #region DEBUG
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(shipData.CurrentHealth.Value);
        }
    }
    #endregion

    protected override void Die()
    {
        gameObject.SetActive(false);
    }
}