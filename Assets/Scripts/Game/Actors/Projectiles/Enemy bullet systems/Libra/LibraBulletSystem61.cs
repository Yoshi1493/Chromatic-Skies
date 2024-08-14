using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem61 : EnemyShooter<EnemyBullet>
{
    const int ParentBulletCount = 6;
    const float ParentBulletSpacing = 360f / ParentBulletCount;
    const int WaveCount = 20;
    const float WaveSpacing = 15f;
    const int ChildBulletCount = 6;
    const float ChildBulletSpacing = 360f / ChildBulletCount;

    List<EnemyBullet> bullets = new(ParentBulletCount);

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        //enabled = false;
        //yield break;

        bullets.Clear();

        for (int i = 0; i < ParentBulletCount; i++)
        {
            float z = i * ParentBulletSpacing;
            Vector3 pos = Vector3.zero;

            var bullet = SpawnProjectile(1, z, pos);
            bullet.Fire();
            bullets.Add(bullet);
        }

        yield return WaitForSeconds(1f);

        for (int i = 0; i < WaveCount; i++)
        {
            bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

            for (int ii = 0; ii < bullets.Count; ii++)
            {
                for (int iii = 0; iii < ChildBulletCount; iii++)
                {
                    float z = (i * WaveSpacing) + (iii * ChildBulletSpacing);
                    Vector3 pos = bullets[ii].transform.position;

                    SpawnProjectile(2, z, pos, false).Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}