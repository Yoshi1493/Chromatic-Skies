using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class AriesShooter01 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const int BranchCount = 15;
    const float BranchSpacing = 8f;
    const int BulletCount = 2;
    const float BulletRotationDuration = 2f;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 1f;

    List<EnemyBullet> bullets = new(BranchCount * BulletCount);

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        bullets.Clear();

        for (int i = 0; i < WaveCount; i++)
        {
            yield return WaitForSeconds(ShootingCooldown);

            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = PlayerPosition.GetRotationDifference(transform.position);
                    float r = (ii - ((iii + BranchCount - 1) / 2f)) * BranchSpacing;
                    float s = BulletBaseSpeed + (iii * BulletSpeedModifier);
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(iii);

                    var bullet = SpawnProjectile(1, z, pos);
                    bullet.StartCoroutine(bullet.RotateBy(r, BulletRotationDuration, delay: 0.5f));
                    bullet.StartCoroutine(bullet.LerpSpeed(0f, s, 1f, delay: iii * 0.5f));
                    bullet.Fire();
                }
            }

            bullets.Clear();
        }
    }
}