using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusShooter01 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 40;
    const float WaveSpacing = 8f;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 3;
    const float BulletSpacing = 5f;
    const float BulletBaseSpeed = 2.5f;
    const float BulletSpeedModifier = 0.5f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            yield return WaitForSeconds(ShootingCooldown);

            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = (i * WaveSpacing) + (ii * BranchSpacing) + (iii * BulletSpacing);
                    float s = BulletBaseSpeed + (iii * BulletSpeedModifier);
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(iii / (BulletCount - 1f));

                    var bullet = SpawnProjectile(1, z, pos);
                    bullet.StartCoroutine(bullet.LerpSpeed(0f, s, 1f, delay: 1f));
                    bullet.Fire();
                }
            }
        }
    }
}