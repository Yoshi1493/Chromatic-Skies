using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem5 : EnemyShooter<Laser>
{
    const int LaserCount = 18;
    const float LaserSpacing = 180f / LaserCount;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);
        
        while (enabled)
        {
            yield return WaitForSeconds(2f);

            for (int i = 0; i < LaserCount; i++)
            {
                float z = i * LaserSpacing + 90f;
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(i / (LaserCount - 1f));
                SpawnProjectile(0, z, pos).Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(2f);
            StartMoveAction?.Invoke();
        }
    }
}