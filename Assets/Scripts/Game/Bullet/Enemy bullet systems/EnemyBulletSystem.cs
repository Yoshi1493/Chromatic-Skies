using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBulletSystem : EnemyShooter
{
    [SerializeField] protected List<EnemyBullet> enemyBullets = new List<EnemyBullet>();

    protected override void Awake()
    {
        base.Awake();
        ownerShip.LoseLifeAction += DestroyAllBullets<EnemyBullet>;
    }

    protected override void OnEnable()
    {
        if (enemyBullets.Count > 0) EnemyBulletPool.Instance.UpdatePoolableBullets(enemyBullets);
        base.OnEnable();
    }

    protected virtual void SpawnBullet(int bulletIndex, int spawnPositionIndex)
    {
        if (!enabled) return;

        var newBullet = EnemyBulletPool.Instance.Get(bulletIndex);
        Transform spawnTransform = spawnPositions[spawnPositionIndex];

        Vector3 posOffset = spawnTransform.up;
        newBullet.transform.SetPositionAndRotation(spawnTransform.position + posOffset, spawnTransform.localRotation);
        newBullet.gameObject.SetActive(true);
        newBullet.enabled = true;
    }
}