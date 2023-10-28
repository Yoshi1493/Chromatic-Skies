using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBullet50 : ScriptableEnemyBullet<CancerBulletSystem5, EnemyBullet>
{
    const int WaveCount = 22;
    const float WaveSpacing = -6f;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletBaseSpeed = 4f;
    const float BulletSpeedMultiplier = 0.1f;
    const float ShootingCooldown = 0.1f;

    protected override float MaxLifetime => Mathf.Infinity;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 0f, 1f);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = (i * WaveSpacing) + (ii * BranchSpacing);
                    float s = BulletBaseSpeed + (i * BulletSpeedMultiplier);
                    Vector3 pos = (2f * Vector3.forward) + transform.position;

                    var bullet = SpawnBullet(1, z, pos, false);
                    bullet.MoveSpeed = s;
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}