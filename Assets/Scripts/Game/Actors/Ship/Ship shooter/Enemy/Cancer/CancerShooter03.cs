using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerShooter03 : CommonEnemyShooter<EnemyBullet>
{
    const int RepeatCount = 3;
    const int WaveCount = 3;
    const float WaveSpacing = 5f;
    const int BranchCount = 8;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 1f;

    protected override float ShootingCooldown => 1.0f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < RepeatCount; i++)
        {
            yield return WaitForSeconds(ShootingCooldown);

            float r = Random.Range(0f, BranchSpacing);

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    float z = (ii * WaveSpacing) + (iii * BranchSpacing) + r;
                    float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(ii / (WaveCount - 1f));

                    var bullet = SpawnProjectile(3, z, pos);
                    bullet.StartCoroutine(bullet.LerpSpeed(3f, s, 1f));
                }

                yield return WaitForSeconds(0.1f);
            }
        }
    }
}