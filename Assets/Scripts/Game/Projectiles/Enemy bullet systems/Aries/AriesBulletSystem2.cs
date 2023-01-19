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

    protected override float ShootingCooldown => 0.08f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            SetSubsystemEnabled(1);

            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int i = 0; i < WaveCount; i++)
            {
                r += i;

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = (ii * BranchSpacing) + ((iii - 1) / 2f * BulletSpacing) + r;
                        Vector3 pos = Vector3.zero;

                        SpawnProjectile(0, z, pos).Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(3f);

            AttackFinishAction?.Invoke();
            yield return WaitForSeconds(1f);
        }
    }
}