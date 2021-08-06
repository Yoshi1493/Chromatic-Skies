using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyLaserSystem : EnemyShooter
{
    [SerializeField] protected List<Laser> enemyLasers = new List<Laser>();

    protected override void Awake()
    {
        base.Awake();
        ownerShip.LoseLifeAction += DestroyAllLasers;
    }

    protected override void OnEnable()
    {
        if (enemyLasers.Count > 0) EnemyLaserPool.Instance.UpdatePoolableBullets(enemyLasers);
        base.OnEnable();
    }

    protected void SpawnLaser()
    {
        if (!enabled) return;

        var newLaser = EnemyLaserPool.Instance.Get(0);

        newLaser.transform.SetPositionAndRotation(spawnPositions[0].position, spawnPositions[0].rotation);
        newLaser.gameObject.SetActive(true);
    }

    void DestroyAllLasers()
    {
        Laser[] lasers = FindObjectsOfType<Laser>();

        for (int i = 0; i < lasers.Length; i++)
        {
            lasers[i].Destroy();
        }
    }
}