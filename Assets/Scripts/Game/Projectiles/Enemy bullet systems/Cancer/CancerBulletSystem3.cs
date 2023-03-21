using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 4;
    const float BulletSpacing = 360f / BulletCount;

    protected override float ShootingCooldown => 3f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);
        StartMoveAction?.Invoke();
        Transform c = transform.GetChild(0);

        yield return WaitForSeconds(1f);

        while (enabled)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                float z = i * BulletSpacing + c.eulerAngles.z;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(0, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}