using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class LeoBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 6;

    [HideInInspector] public List<Vector3> bulletSpawnPositions = new(BulletCount);

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            bulletSpawnPositions.Clear();

            StartMoveAction?.Invoke();

            for (int i = 0; i < BulletCount; i++)
            {
                float x = Mathf.Lerp(-screenHalfWidth, screenHalfWidth, i / (BulletCount - 1f)) + Random.Range(-1f, 1f);
                float y = Random.Range(1f, screenHalfHeight);

                Vector3 v = 0.8f * new Vector3(x, y);
                bulletSpawnPositions.Add(v);
            }

            for (int i = 0; i < BulletCount; i++)
            {
                float z = 0f;
                Vector3 pos = Vector3.zero;

                var bullet = SpawnProjectile(0, z, pos);
                bullet.StartCoroutine(bullet.MoveTo(bulletSpawnPositions[i], 1f));
                bullet.Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(9f);
        }
    }
}