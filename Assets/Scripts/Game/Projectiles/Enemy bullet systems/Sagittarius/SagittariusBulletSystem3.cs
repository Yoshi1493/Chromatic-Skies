using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 30;
    const float WaveSpacing = 1f;
    const int BranchCount = 10;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 0.03f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = (i * WaveSpacing) + (ii * BranchSpacing);
                    Vector3 pos = 0.5f * transform.up.RotateVectorBy(z + 90f);

                    bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));
                    var bullet = SpawnProjectile(0, z, pos);
                    bullet.MoveSpeed = 0.2f * i + 3f;
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            //yield return ownerShip.MoveToRandomPosition(1f, delay: 10f);
        }
    }
}