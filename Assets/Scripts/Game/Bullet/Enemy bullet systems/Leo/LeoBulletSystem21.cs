using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 2;
    const int BulletCount = 8;
    const int SpawnBranchCount = 3;
    const int SpawnBranchSpacing = 360 / SpawnBranchCount;
    const float halfPI = 0.5f * Mathf.PI;

    List<EnemyBullet> bullets = new(BulletCount * 4 * BranchCount - BranchCount);

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        //lemniscate (infinity curve) (x^2 + y^2)^2 - rx^2 + ry^2 = 0, modified and broken into 3 parts:
        //positive sine wave:  f(x) = sin(x)      {1 < x < t}
        //semicircle:          1 = (x-t)^2 + y^2  {x > t}
        //negative sine wave:  f(x) = -sin(x)     {-t < x < -1}
        //where t = pi / 2
        while (enabled)
        {
            float step = halfPI / BulletCount;
            float x, y, z;

            for (int i = 1; i < BulletCount; i++)
            {
                yield return WaitForSeconds(ShootingCooldown);

                x = i * step;
                y = Mathf.Sin(x);
                Vector3 pos = new(x, y);

                SpawnBullets(pos);
            }

            for (int i = 0; i < BulletCount * 2; i++)
            {
                yield return WaitForSeconds(ShootingCooldown);

                Vector3 offset = halfPI * Vector3.right;
                float theta = i * -90f / BulletCount;
                Vector3 pos = -transform.up.RotateVectorBy(theta) + offset;

                SpawnBullets(pos);
            }

            for (int i = BulletCount; i > 0; i--)
            {
                yield return WaitForSeconds(ShootingCooldown);

                x = i * step;
                y = -Mathf.Sin(x);
                Vector3 pos = new(x, y);

                SpawnBullets(pos);
            }

            yield return WaitForSeconds(2f - (ShootingCooldown * bullets.Capacity * 0.5f));

            for (int i = 0; i < bullets.Count; i++)
            {
                float randOffset = Random.Range(0f, SpawnBranchSpacing);

                for (int j = 0; j < SpawnBranchCount; j++)
                {
                    SpawnProjectile(3, j * SpawnBranchSpacing + randOffset, bullets[i].transform.position, false).Fire();
                }

                if (i % 2 == 1)
                    yield return WaitForSeconds(ShootingCooldown);
            }

            bullets.Clear();

            enabled = false;
        }
    }

    void SpawnBullets(Vector3 pos)
    {
        float z = pos.GetRotationDifference(Vector3.zero);

        var bullet = SpawnProjectile(2, z, pos);
        bullets.Add(bullet);
        bullet.Fire();

        z += 180f;

        bullet = SpawnProjectile(2, z, -pos);
        bullets.Add(bullet);
        bullet.Fire();
    }
}