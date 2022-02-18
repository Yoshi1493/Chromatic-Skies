using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem11 : EnemyBulletSubsystem<EnemyBullet>
{
    readonly int BulletCount = 128;
    readonly float Spacing = 12f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);
        float rand = Random.Range(0f, 180f);

        for (int i = 0; i < BulletCount; i++)
        {
            float z = i * Spacing + rand;
            SpawnProjectile(1, z, Vector3.down.RotateVectorBy(z * 0.5f)).Fire();

            z += 180;
            SpawnProjectile(1, z, Vector3.down.RotateVectorBy(z * 0.5f)).Fire();

            yield return WaitForSeconds(ShootingCooldown / 4f);
        }

        enabled = false;
    }
}