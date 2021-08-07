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

    protected void SpawnLaser(float zRotation, Vector2 offset)
    {
        if (!enabled) return;

        var newLaser = EnemyLaserPool.Instance.Get(0);

        newLaser.transform.SetPositionAndRotation(ShipPosition + offset, Quaternion.Euler(0, 0, zRotation));
        newLaser.gameObject.SetActive(true);
        newLaser.enabled = true;
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