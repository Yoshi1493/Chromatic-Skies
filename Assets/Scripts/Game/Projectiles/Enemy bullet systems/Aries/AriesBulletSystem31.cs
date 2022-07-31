using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem31 : EnemyBulletSubsystem<EnemyBullet>
{
    const float InnerRadius = 2f;
    const int InnerBulletCount = 12;
    const float InnerBulletSpacing = 360f / InnerBulletCount;
    const float OuterRadius = 2.5f;
    const int OuterBulletCount = 16;
    const float OuterBulletSpacing = 360f / OuterBulletCount;

    List<EnemyBullet> bullets = new(InnerBulletCount + OuterBulletCount);

    protected override float ShootingCooldown => 0.025f;

    protected override IEnumerator Shoot()
    {
        transform.position = 2f * Vector3.down;

        for (int i = 0; i < InnerBulletCount; i++)
        {
            float z = i * -InnerBulletSpacing + 90f;
            Vector3 pos = InnerRadius * transform.up.RotateVectorBy(z + 90f);

            bullets.Add(SpawnProjectile(1, z, pos));

            yield return WaitForSeconds(ShootingCooldown);
        }

        yield return WaitForSeconds(0.1f);

        for (int i = 0; i < OuterBulletCount; i++)
        {
            float z = i * OuterBulletSpacing - 90f;
            Vector3 pos = OuterRadius * transform.up.RotateVectorBy(z - 90f);

            bullets.Add(SpawnProjectile(2, z, pos));

            yield return WaitForSeconds(ShootingCooldown);
        }

        yield return WaitForSeconds(0.1f);

        bullets.ForEach(b => b.Fire());
        bullets.Clear();
    }
}