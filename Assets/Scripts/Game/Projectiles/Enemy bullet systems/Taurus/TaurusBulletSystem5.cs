using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem5 : EnemyShooter<EnemyBullet>
{
    readonly Vector2 MinBounds = new(-7f, 0f);
    readonly Vector2 MaxBounds = new(7f, 4f);
    const int BulletRowCount = 4;
    const int BulletColCount = 14;
    const int BulletCount = 5;

    List<List<Vector2>> bulletSpawnPositions = new();
    List<EnemyBullet> bullets = new(BulletRowCount * BulletColCount);

    protected override float ShootingCooldown => 0.05f;

    protected override void Awake()
    {
        base.Awake();

        bulletSpawnPositions.Clear();

        for (int i = 0; i <= BulletColCount; i++)
        {
            bulletSpawnPositions.Add(new());
            float x = Mathf.Lerp(MinBounds.x, MaxBounds.x, i / (float)BulletColCount);

            for (int ii = 0; ii <= BulletRowCount; ii++)
            {
                float y = Mathf.Lerp(MinBounds.y, MaxBounds.y, ii / (float)BulletRowCount);
                bulletSpawnPositions[i].Add(new(x, y));
            }
        }
    }

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        for (int i = 1; enabled; i *= -1)
        {
            float z = 180f;

            for (int ii = 0; ii < bulletSpawnPositions.Count; ii++)
            {
                for (int iii = 0; iii < bulletSpawnPositions[ii].Count; iii++)
                {
                    Vector3 pos = bulletSpawnPositions[ii][iii];
                    pos.x *= i;

                    if ((ii + iii) % 2 == 1)
                    {
                        int b = Random.Range(0, BulletCount);
                        bullets.Add(SpawnProjectile(b, z, pos, false));
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            bullets.Randomize();
            yield return WaitForSeconds(1f);

            StartCoroutine(FireBullets());
            yield return WaitForSeconds(5f);
        }
    }

    IEnumerator FireBullets()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].Fire();
            yield return WaitForSeconds(ShootingCooldown * 2f);
        }

        bullets.Clear();
    }
}