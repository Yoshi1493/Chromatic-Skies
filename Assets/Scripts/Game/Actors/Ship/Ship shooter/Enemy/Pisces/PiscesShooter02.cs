using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesShooter02 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 8;
    const int BranchCount = 1;
    const float BranchSpacing = 10f;
    const int BulletCount = 3;
    const float BulletRotationSpeed = 5f;
    const float BulletRotationDuration = 1f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        for (int i = 0; i < WaveCount; i++)
        {
            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = ((ii - ((BranchCount - 1) / 2f)) * BranchSpacing) + r;
                    Vector3 pos = Vector3.zero;

                    var bullet = SpawnProjectile(0, z, pos);
                    bullet.StartCoroutine(bullet.RotateBy(Random.Range(-BulletRotationSpeed, BulletRotationSpeed), BulletRotationDuration));
                    bullet.Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}