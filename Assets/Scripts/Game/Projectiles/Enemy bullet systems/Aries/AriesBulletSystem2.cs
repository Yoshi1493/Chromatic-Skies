using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 44;
    const float WaveSpacing = 11f;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletBaseSpeed = 4f;
    const float BulletSpeedMultiplier = 0.08f;
    const float BulletRotationSpeed = 270f;

    protected override float ShootingCooldown => 0.15f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                bulletData.colour = bulletData.gradient.Evaluate(Mathf.PingPong(i / (WaveCount - 1f), 1f));

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = (i * WaveSpacing) + (ii * BranchSpacing);
                        float s = BulletBaseSpeed + (i * BulletSpeedMultiplier);
                        Vector3 pos = Vector3.zero;

                        var bullet = SpawnProjectile(0, z, pos);
                        bullet.MoveSpeed = s;
                        bullet.StartCoroutine(bullet.RotateBy(BulletRotationSpeed, 1f, iii % BulletCount == 0));
                        bullet.Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            StartMoveAction?.Invoke();
            SetSubsystemEnabled(1);

            yield return WaitForSeconds(1f);
        }
    }
}