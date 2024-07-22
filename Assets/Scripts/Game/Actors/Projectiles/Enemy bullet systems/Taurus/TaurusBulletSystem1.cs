using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const float WaveSpacing = BranchSpacing / 2f;
    const int BranchCount = 18;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 1f;
    const float BulletRotationSpeed = 60f;
    const float BulletRotationDuration = 5f;

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);
        SetSubsystemEnabled(2);

        while (enabled)
        {
            yield return WaitForSeconds(2f);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = (i * WaveSpacing) + ((ii + (iii * 0.5f)) * BranchSpacing);
                        float s = BulletBaseSpeed + (iii * BulletSpeedModifier);
                        Vector3 pos = Vector3.zero;

                        bulletData.colour = bulletData.gradient.Evaluate(iii);

                        var bullet = SpawnProjectile(0, z, pos);
                        bullet.MoveSpeed = s;
                        bullet.StartCoroutine(bullet.RotateBy((iii % 2 * 2 - 1) * BulletRotationSpeed, BulletRotationDuration));
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);

            SetSubsystemEnabled(3);
            yield return WaitForSeconds(1f);

            StartMoveAction?.Invoke();
        }
    }
}