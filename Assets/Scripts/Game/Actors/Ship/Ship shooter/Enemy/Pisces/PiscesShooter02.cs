using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesShooter02 : CommonEnemyShooter<EnemyBullet>
{
    const int RepeatCount = 2;
    const int WaveCount = 24;
    const float WaveSpacing = 2f;
    const int BranchCount = 12;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.1f;
    const float BulletRotationSpeed = 10f;
    const float BulletRotationDuration = 1f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        for (int i = 0; i < RepeatCount; i++)
        {
            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    float z = (i % 2 * 2 - 1) * ((ii * WaveSpacing) + (iii * BranchSpacing));
                    float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(ii / (WaveCount - 1f));

                    var bullet = SpawnProjectile(2, z, pos);
                    bullet.StartCoroutine(bullet.LerpSpeed(BulletBaseSpeed, s, 1f, delay: 1f));
                    bullet.StartCoroutine(bullet.RotateBy(Random.Range(-BulletRotationSpeed, BulletRotationSpeed), BulletRotationDuration, delay: 1.2f));
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(2f);
        }
    }
}