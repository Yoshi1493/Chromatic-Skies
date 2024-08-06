using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem63 : EnemyShooter<EnemyBullet>
{
    public const int ParentBulletCount = 8;
    public const float ParentBulletSpacing = 360f / ParentBulletCount;

    List<EnemyBullet> bullets = new(ParentBulletCount);

    protected override IEnumerator Shoot()
    {
        //enabled = false;
        //yield break;

        for (int i = 0; i < ParentBulletCount; i++)
        {
            float z = (i + 0.5f) * ParentBulletSpacing;
            Vector3 pos = Vector3.zero;

            var bullet = SpawnProjectile(5, z, pos);
            bullet.Fire();
            bullets.Add(bullet);
        }

        yield return WaitForSeconds(1f);
        SetSubsystemEnabled(1);

        enabled = false;
    }
}