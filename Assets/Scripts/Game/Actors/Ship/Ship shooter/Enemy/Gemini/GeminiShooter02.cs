using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiShooter02 : CommonEnemyShooter<EnemyBullet>
{
    const int RepeatCount = 2;
    const int WaveCount = 32;
    const float BranchCount = 2;
    public const float BranchSpacing = 60f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        for (int i = 0; i < RepeatCount; i++)
        {
            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    int d = iii % 2 * 2 - 1;
                    float z = d * BranchSpacing + r;
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(ii % 2);

                    var bullet = SpawnProjectile(2, z, pos);
                    bullet.MoveSpeed = d;
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(2f);
        }
    }
}