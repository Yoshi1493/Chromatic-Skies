using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem31 : EnemyBulletSubsystem<EnemyBullet>
{
    const int InnerBulletCount = 12;
    const float InnerBulletSpacing = 360f / InnerBulletCount;
    const int OuterBulletCount = 16;
    const float OuterBulletSpacing = 360f / OuterBulletCount;

    List<EnemyBullet> bullets = new List<EnemyBullet>(InnerBulletCount + OuterBulletCount);

    protected override float ShootingCooldown => 0.025f;

    protected override IEnumerator Shoot()
    {
        transform.position = 2f * Vector3.down;

        for (int i = 0; i < InnerBulletCount; i++)
        {
            float z = i * -InnerBulletSpacing + 90f;
            bullets.Add(SpawnProjectile(1, z, transform.up.RotateVectorBy(z + 90f) * 2f));

            yield return WaitForSeconds(ShootingCooldown);
        }

        yield return WaitForSeconds(ShootingCooldown * 4f);

        for (int i = 0; i < OuterBulletCount; i++)
        {
            float z = i * OuterBulletSpacing - 90f;
            bullets.Add(SpawnProjectile(2, z, transform.up.RotateVectorBy(z - 90f) * 2.5f));

            yield return WaitForSeconds(ShootingCooldown);
        }

        bullets.ForEach(b => b.Fire());
        bullets.Clear();
    }
}