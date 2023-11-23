using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 66;
    const float WaveSpacing = -6f;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletBaseSpeed = 4f;
    const float BulletSpeedModifier = 0.1f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = (i * WaveSpacing) + (ii * BranchSpacing);
                    float s = BulletBaseSpeed + (i * BulletSpeedModifier);
                    Vector3 pos = (2f * Vector3.forward) + transform.position;

                    var bullet = SpawnProjectile(0, z, pos, false);
                    bullet.MoveSpeed = s;
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1.5f);

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(1f);
        }
    }
}