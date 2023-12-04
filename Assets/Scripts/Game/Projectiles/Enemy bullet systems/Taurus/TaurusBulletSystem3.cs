using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem3 : EnemyShooter<Laser>
{
    const int LaserCount = 12;
    const float LaserSpacing = 360f / LaserCount;

    protected override float ShootingCooldown => 5f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            yield return WaitForSeconds(ShootingCooldown);

            float r = transform.position.GetRotationDifference(PlayerPosition);

            for (int i = 0; i < LaserCount; i++)
            {
                float z = (i * LaserSpacing) + r;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(0, z, pos).Fire();
            }

            for (int i = 0; i < 3; i++)
            {
                SetSubsystemEnabled(2);
                StartMoveAction?.Invoke();

                yield return WaitForSeconds(0.5f);
            }
        }
    }
}