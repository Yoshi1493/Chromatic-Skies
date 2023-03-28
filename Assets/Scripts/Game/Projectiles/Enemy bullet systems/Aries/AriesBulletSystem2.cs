using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 111;
    const int BranchCount = 3;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletSpacing = BranchSpacing / 2f;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedMultiplier = 0.01f;

    protected override float ShootingCooldown => 0.08f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            float t = 0f;
            float r = Random.Range(0f, BranchSpacing);

            for (int i = 0; i < WaveCount; i++)
            {
                t += i;

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = (ii * BranchSpacing) + ((iii - 1) / 2f * BulletSpacing) + t + r;
                        float s = BulletBaseSpeed + (i * BulletSpeedMultiplier);
                        Vector3 pos = Vector3.zero;

                        var bullet = SpawnProjectile(0, z, pos);
                        bullet.MoveSpeed = s;
                        bullet.Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(1f);
        }
    }
}