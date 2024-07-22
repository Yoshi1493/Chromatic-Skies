using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 6;
    readonly float BulletSpacing = (1f + Mathf.Sqrt(5f)) * 180f;
    const float BulletSpawnRadius = 1.0f;
    const float SpawnRadiusModifier = 0.002f;

    protected override float ShootingCooldown => 1f / 30;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        StartMoveAction?.Invoke();
        SetSubsystemEnabled(1);

        for (int i = 0; enabled; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                int b = i % 2;
                float z = i * BulletSpacing;
                Vector3 pos = Mathf.PingPong(i * SpawnRadiusModifier, BulletSpawnRadius) * transform.up.RotateVectorBy(z);


                SpawnProjectile(b, z, pos).Fire();
                i++;
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}