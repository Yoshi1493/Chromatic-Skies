using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem63 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return WaitForSeconds(ShootingCooldown * 5f);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float x = 3f;
                    float y = screenHalfHeight * 1.1f;
                    float z = ii * BranchSpacing;
                    Vector3 pos = new Vector3(x, y).RotateVectorBy(z);

                    var bullet = SpawnProjectile(2, z, pos, false);
                    bullet.MoveSpeed = 2.4f;
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}