using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 8;
    const float halfPI = 0.5f * Mathf.PI;

    protected override float ShootingCooldown => 0.02f;

    protected override IEnumerator Shoot()
    {
        //lemniscate (infinity curve) (x^2 + y^2)^2 - rx^2 + ry^2 = 0, modified and broken into 5 parts:
        //positive sine wave front half: f(x) = sin(x) {0 < x < t}
        //right semicircle: 1 = (x-t)^2 + y^2 {x > t}
        //negative sine wave: f(x) = -sin(x) {-t < x < t}
        //left semicircle: 1 = (x+t)^2 + y^2 {x < -t}
        //positive sine wave back half: f(x) = sin(x) {-t < x < 0}
        //where t = pi / 2
        while (enabled)
        {
            float step = halfPI / BulletCount;
            float x, y, z = 0f;

            for (int i = 0; i < BulletCount; i++)
            {
                x = i * step;
                y = Mathf.Sin(x);

                SpawnProjectile(0, z, new Vector3(x, y)).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }

            for (int i = 0; i < BulletCount * 2; i++)
            {
                Vector3 offset = halfPI * Vector3.right;
                float theta = i * -90f / BulletCount;

                SpawnProjectile(0, z, -transform.up.RotateVectorBy(theta) + offset).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }

            for (int i = BulletCount; i > -BulletCount; i--)
            {
                x = i * step;
                y = -Mathf.Sin(x);

                SpawnProjectile(0, z, new Vector3(x, y)).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }

            for (int i = 0; i < BulletCount * 2; i++)
            {
                Vector3 offset = halfPI * Vector3.left;
                float theta = i * 90f / BulletCount;

                SpawnProjectile(0, z, -transform.up.RotateVectorBy(theta) + offset).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }

            for (int i = -BulletCount; i < 0; i++)
            {
                x = i * step;
                y = Mathf.Sin(x);

                SpawnProjectile(0, z, new Vector3(x, y)).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }

            enabled = false;
        }

    }
}