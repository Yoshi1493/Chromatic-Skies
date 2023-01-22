using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 18;
    const float BranchSpacing = 180f / BranchCount;
    const int BulletCount = 2;
    const float MinBulletSpacing = 15f;
    const float MaxBulletSpacing = 30f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            SetSubsystemEnabled(1);

            for (int i = BranchCount - 1; i >= 0; i--)
            {
                float r = Random.Range(MinBulletSpacing, MaxBulletSpacing);

                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = (i * BranchSpacing) + (ii * r);
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(0, z, pos).Fire();

                    if (i != 0)
                    {
                        SpawnProjectile(0, -z, pos).Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(2f);

            AttackFinishAction?.Invoke();
            yield return WaitForSeconds(1f);
        }
    }
}