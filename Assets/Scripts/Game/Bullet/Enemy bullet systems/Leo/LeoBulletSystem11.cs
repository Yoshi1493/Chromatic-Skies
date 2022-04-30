using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem11 : EnemyBulletSubsystem<EnemyBullet>
{
    const int BulletCount = 150;
    const float BulletSpacing = 12f;

    protected override float ShootingCooldown => 0.03f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);

        float randOffset = Random.Range(0f, 180f);
        float randDirection = Mathf.Sign(Random.value - 0.5f);

        for (int i = 0; i < BulletCount; i++)
        {
            float z = (i * BulletSpacing * randDirection) + randOffset;
            SpawnProjectile(1, z, Vector3.up.RotateVectorBy(z * 0.5f)).Fire();

            z += 180f;
            SpawnProjectile(1, z, Vector3.up.RotateVectorBy(z * 0.5f)).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}