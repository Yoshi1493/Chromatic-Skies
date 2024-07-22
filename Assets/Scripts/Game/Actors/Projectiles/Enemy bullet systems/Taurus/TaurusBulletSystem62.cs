using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem62 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 5;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 13/60f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float x = 4f;
                    float y = screenHalfHeight * 1.1f;
                    float z = ii * BranchSpacing;
                    Vector3 pos = new Vector3(x, y).RotateVectorBy(z);

                    var bullet = SpawnProjectile(1, z, pos, false);
                    bullet.MoveSpeed = 3f;
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);
        }
    }
}