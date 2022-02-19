using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem11 : EnemyBulletSubsystem<EnemyBullet>
{
    readonly int BulletCount = 144;
    readonly float Spacing = 12f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);
        float randOffset = Random.Range(0f, 180f);
        float randDirection = Mathf.Sign(Random.value - 0.5f);

        for (int i = 0; i < BulletCount; i++)
        {
            float z = (i * Spacing * randDirection) + randOffset;
            SpawnProjectile(1, z, Vector3.up.RotateVectorBy(z * 0.5f)).Fire();

            z += 180;
            SpawnProjectile(1, z, Vector3.up.RotateVectorBy(z * 0.5f)).Fire();

            yield return WaitForSeconds(ShootingCooldown / 4f);
        }

        enabled = false;
    }
}