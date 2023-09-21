using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem61 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 6;

    protected override float ShootingCooldown => 3f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        List<EnemyBullet> bullets = new(BulletCount);
        List<Vector3> bulletPosData = new(BulletCount);

        for (int i = 0; i < BulletCount; i++)
        {
            float z = 0f;
            Vector3 pos = Vector3.zero;

            bulletData.colour = bulletData.gradient.Evaluate(i / (BulletCount - 1f));
            bullets.Add(SpawnProjectile(1, z, pos));
        }

        while (enabled)
        {
            for (int i = 0; i < BulletCount / 2; i++)
            {
                Vector3 pos = GetRandomCoordinates();

                while (bulletPosData.Exists(b => b.x - pos.x == b.y - pos.y))
                {
                    pos = GetRandomCoordinates();
                }

                bulletPosData.Add(pos);
            }

            for (int i = 0; i < BulletCount / 2; i++)
            {
                Vector3 pos = bulletPosData[i];
                pos.x *= -1;
                bulletPosData.Add(pos);
            }

            for (int i = 0; i < BulletCount; i++)
            {
                bullets[i].StartCoroutine(bullets[i].MoveTo(bulletPosData[i], 0.5f));
            }

            yield return WaitForSeconds(1f);

            bullets.ForEach(b => b.Fire());

            yield return WaitForSeconds(ShootingCooldown);
            bulletPosData.Clear();
        }
    }

    Vector3 GetRandomCoordinates()
    {
        int x = Random.Range(4, 8);
        int y = Random.Range(1, 5);
        return new(x, y);
    }
}