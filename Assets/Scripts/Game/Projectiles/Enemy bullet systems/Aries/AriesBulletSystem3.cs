using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem3 : EnemyShooter<EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;

    const int BulletCount = 6;
    const float BulletSpacing = 180f / BulletCount;

    protected override float ShootingCooldown => 0.3f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);
        yield return WaitForSeconds(1f);

        while (enabled)
        {
            float r = Random.Range(-90f, 90f);

            for (int i = 0; i <= BulletCount; i++)
            {
                float z = r + (i * BulletSpacing) - 120f;
                Vector3 pos = transform.up.RotateVectorBy(r);

                bulletData.colour = bulletData.gradient.Evaluate(i / (BulletCount - 1f));
                SpawnProjectile(0, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}