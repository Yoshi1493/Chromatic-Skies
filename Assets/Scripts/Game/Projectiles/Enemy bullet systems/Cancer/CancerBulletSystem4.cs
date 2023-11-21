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

        float z = 0f;
        Vector3 pos = Vector3.zero;

        SpawnProjectile(0, z, pos).Fire();

        yield return WaitForSeconds(1f);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    z = (i * WaveSpacing) + (ii * BranchSpacing);
                    float s = BulletBaseSpeed + (i * BulletSpeedModifier);
                    pos = (2f * Vector3.forward) + transform.position;

                    var bullet = SpawnProjectile(1, z, pos, false);
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