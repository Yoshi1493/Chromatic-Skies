using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesShooter00 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const int BranchCount = 6;
    const int BulletCount = 16;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 1f;

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
                    float z = (ii * BranchSpacing) + r;
                    float s = Random.Range(1f, 2f);
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(Mathf.InverseLerp(1f, 2f, s));

                    var bullet = SpawnProjectile(0, z, pos);
                    bullet.StartCoroutine(bullet.LerpSpeed(0.5f, s, s, delay: 0.5f));
                    bullet.Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}