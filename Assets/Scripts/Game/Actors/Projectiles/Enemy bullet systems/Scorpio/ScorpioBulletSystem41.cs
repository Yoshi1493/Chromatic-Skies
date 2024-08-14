using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem41 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 4;
    const int BranchCount = 20;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 4;
    const float BulletSpacing = 5f;
    const float BulletBaseSpeed = 1.6f;
    const float BulletSpeedModifier = 0.2f;
    const float BulletRotationSpeed = 12f;
    const float BulletRotationDuration = 2.5f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            int d = i % 2 * 2 - 1;

            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = (ii * BranchSpacing) + (d * (iii * BulletSpacing));
                    float s = BulletBaseSpeed + (iii * BulletSpeedModifier);
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(i % 2);

                    var bullet = SpawnProjectile(2, z, pos);
                    bullet.MoveSpeed = s;
                    bullet.StartCoroutine(bullet.RotateBy(d * iii * BulletRotationSpeed, BulletRotationDuration, delay: 1f));
                    bullet.Fire();
                }
            }

            yield return WaitForSeconds(0.8f);
        }

        enabled = false;
    }
}