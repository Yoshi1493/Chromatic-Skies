using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBulletSystem5 : EnemyShooter<EnemyBullet>
{
    public const float WaveSpacing = 15f;
    public const int BranchCount = 5;
    public const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 0.4f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SpawnProjectile(0, 0f, Vector3.zero).Fire();

        StartMoveAction?.Invoke();
        yield return WaitForSeconds(2f);

        for (int i = 0; enabled; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = -(i * WaveSpacing) + (ii * BranchSpacing);
                Vector3 pos = Vector3.zero;

                SpawnProjectile(1, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}