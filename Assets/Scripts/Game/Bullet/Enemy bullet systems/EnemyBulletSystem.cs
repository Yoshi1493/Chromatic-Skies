using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBulletSystem : EnemyShooter
{
    [SerializeField] protected List<EnemyBullet> enemyBullets = new List<EnemyBullet>();

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        for (int i = 0; i < enemyBullets.Count; i++) { enemyBullets[i].BulletIndex = i; }
        if (enemyBullets.Count > 0) EnemyBulletPool.Instance.UpdatePoolableBullets(enemyBullets);

        ownerShip.LoseLifeAction += OnLoseLife;
    }

    protected virtual void SpawnBullet(int bulletIndex, float zRotation, Vector2 offset)
    {
        var newBullet = EnemyBulletPool.Instance.Get(bulletIndex);

        newBullet.transform.SetPositionAndRotation(ShipPosition + offset, Quaternion.Euler(0, 0, zRotation));
        newBullet.gameObject.SetActive(true);
        newBullet.enabled = true;
    }

    void OnLoseLife()
    {
        StopCoroutine(shootCoroutine);
        DestroyAllBullets<EnemyBullet>();
    }
}