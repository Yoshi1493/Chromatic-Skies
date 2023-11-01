using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 9;
    const int BranchCount = 24;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletBaseSpeed = 1f;
    const float BulletSpeedModifier = 0.1f;
    const float MaxShootingCooldown = 0.5f;

    protected override float ShootingCooldown => 4f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            float sc = MaxShootingCooldown;

            for (int i = 0; i < WaveCount; i++)
            {
                float s = BulletBaseSpeed + (i * BulletSpeedModifier);
                Vector3 pos = Random.insideUnitCircle;

                bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = ii * BranchSpacing;

                    var bullet = SpawnProjectile(0, z, pos);
                    bullet.MoveSpeed = s;
                    bullet.Fire();
                }

                yield return WaitForSeconds(sc);
                sc -= MaxShootingCooldown * 0.1f;
            }

            StartMoveAction?.Invoke();
            SetSubsystemEnabled(1);

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}