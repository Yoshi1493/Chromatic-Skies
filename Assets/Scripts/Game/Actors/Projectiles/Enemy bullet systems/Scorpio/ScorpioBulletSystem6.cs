using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class ScorpioBulletSystem6 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 6;

    List<EnemyBullet> bullets = new(BulletCount);

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        //SetSubsystemEnabled(1);

        while (enabled)
        {
            bullets.Clear();

            //yield return WaitForSeconds(3f);

            SpawnProjectile(1, 0f, Vector3.zero).Fire();

            yield return WaitForSeconds(1f);

            for (int i = 0; i < BulletCount; i++)
            {
                float z = PlayerPosition.GetRotationDifference(transform.position);
                Vector3 pos = transform.position;

                var bullet = SpawnProjectile(2, z, pos, false);
                bullets.Add(bullet);
                bullet.Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(3f);

            SpawnProjectile(0, 0f, Vector3.zero).Fire();
            yield return WaitForSeconds(2f);

            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].isActiveAndEnabled)
                {
                    bullets[i].GetComponent<ITimestoppable>().Resume();
                }
            }

            break;
        }
    }
}