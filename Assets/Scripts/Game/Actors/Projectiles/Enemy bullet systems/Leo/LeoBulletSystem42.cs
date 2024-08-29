using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem42 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 25;
    const float WaveSpacing = BranchSpacing / (WaveCount - 1);
    const int BranchCount = 5;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float SpawnRadiusModifier = 0.05f;
    const float BulletBaseSpeed = 3f;
    const float BulletSpeedModifier = -0.06f;
    const float BulletRotationSpeed = 90f;
    const float BulletRotationDuration = 4f;

    protected override float ShootingCooldown => 0.1f;

    protected override IEnumerator Shoot()
    {
        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    int d = iii % 2 * 2 - 1;
                    float z = (d * ((i * WaveSpacing) + (ii * BranchSpacing))) + r;
                    float s = BulletBaseSpeed + (i * BulletSpeedModifier);
                    Vector3 pos = i * SpawnRadiusModifier * transform.up.RotateVectorBy(z);

                    bulletData.colour = bulletData.gradient.Evaluate(iii);

                    var bullet = SpawnProjectile(2, z, pos);
                    bullet.StartCoroutine(bullet.LerpSpeed(1f, s, 1f));
                    bullet.StartCoroutine(bullet.RotateBy(d * BulletRotationSpeed, BulletRotationDuration));
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}