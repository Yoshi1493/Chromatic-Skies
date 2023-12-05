using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const int LaserCount = 2;
    const float LaserSpacing = 360f / LaserCount;

    protected override float ShootingCooldown => 4f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);
        SetSubsystemEnabled(2);

        while (enabled)
        {
            yield return WaitForSeconds(ShootingCooldown);
            StartMoveAction?.Invoke();
        }
    }
}