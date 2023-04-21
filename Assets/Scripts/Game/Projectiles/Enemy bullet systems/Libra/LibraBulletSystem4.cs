using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 0.04f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            StartMoveAction?.Invoke();

            Vector3 p = transform.position;

            yield return WaitForSeconds(ShootingCooldown);

            while (transform.position != p)
            {
                Vector3 d = transform.position - p;

                for (int i = 0; i < BranchCount; i++)
                {
                    float z = (i * BranchSpacing) + Vector3.zero.GetRotationDifference(d.RotateVectorBy(90f));
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(0, z, pos).Fire();
                }

                p = transform.position;

                yield return WaitForSeconds(ShootingCooldown);
            }

            SetSubsystemEnabled(1);
            yield return WaitForSeconds(5f);
        }
    }
}