using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 6;
    const float BulletSpacing = 0.8f;

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int i = 1; i <= BranchCount; i++)
            {
                float x = (i - 1) * BulletSpacing * 0.5f;

                for (int ii = 0; ii < i; ii++)
                {
                    Vector3 t = (ii * BulletSpacing - x) * Vector3.right;

                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = iii * BranchSpacing + r;
                        Vector3 pos = t.RotateVectorBy(z);
                        bulletData.colour = bulletData.gradient.Evaluate(i / (float)BranchCount);

                        SpawnProjectile(0, z, pos).Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            SetSubsystemEnabled(1);
            yield return WaitForSeconds(1f);

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(5f);
        }
    }
}