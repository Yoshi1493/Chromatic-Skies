using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const float WaveSpacing = BranchSpacing / 2f;
    const int BranchCount = 48;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const int BulletClumpCount = 3;
    const float BulletRotationSpeed = 10f;
    const float BulletRotationDuration = 2f;

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            SetSubsystemEnabled(1);
            yield return WaitForSeconds(1f);

            for (int i = 0; i < WaveCount; i++)
            {
                StartMoveAction?.Invoke();

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        int b = iii % 2;
                        float r = (((i % 2) + ii + iii) % BulletClumpCount - ((BulletClumpCount - 1) / 2f)) * BulletRotationSpeed;
                        float z = (i * WaveSpacing) + (ii * BranchSpacing);
                        Vector3 pos = Vector3.zero;

                        var bullet = SpawnProjectile(b, z, pos);
                        bullet.StartCoroutine(bullet.RotateBy(r, BulletRotationDuration, delay: 1f));
                        bullet.Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(3f);
        }
    }
}