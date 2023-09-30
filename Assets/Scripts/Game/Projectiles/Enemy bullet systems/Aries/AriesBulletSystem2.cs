using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 50;
    const float WaveSpacing = 8f;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletBaseSpeed = 3f;
    const float BulletSpeedMultiplier = 0.02f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            float r = Random.Range(0f, BranchSpacing);

            for (int i = 0; i < WaveCount; i++)
            {
                bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = (i * WaveSpacing) + (ii * BranchSpacing) + r;
                        float s = BulletBaseSpeed + (i * BulletSpeedMultiplier);
                        Vector3 pos = Vector3.zero;

                        var bullet = SpawnProjectile(0, z, pos);
                        bullet.MoveSpeed = s;
                        bullet.StartCoroutine(bullet.RotateBy(270f, 2f, iii % BulletCount == 0));
                        bullet.Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);

            StartMoveAction?.Invoke();
            SetSubsystemEnabled(1);

            yield return WaitForSeconds(1f);
        }
    }
}