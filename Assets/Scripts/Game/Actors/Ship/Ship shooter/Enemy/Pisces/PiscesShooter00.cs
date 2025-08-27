using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesShooter00 : CommonEnemyShooter<EnemyBullet>
{
    const int RepeatCount = 3;
    const int WaveCount = 24;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        float r = PlayerPosition.GetRotationDifference(transform.position);
        for (int i = 0; i < RepeatCount; i++)
        {
            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    float z = (i * 0.5f * BranchSpacing) + (iii * BranchSpacing) + r;
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