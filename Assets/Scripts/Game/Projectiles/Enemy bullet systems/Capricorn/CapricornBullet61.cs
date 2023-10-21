using System.Collections;
using UnityEngine;

public class CapricornBullet61 : ScriptableEnemyBullet<CapricornBulletSystem6, EnemyBullet>
{
    const int WaveCount = 12;
    const float WaveSpacing = 360f / BranchCount / WaveCount;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const float ShootingCooldown = 0.05f;
    const float BulletBaseSpeed = 2f;

    [SerializeField] ProjectileObject bulletData;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 0f, 0.5f);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BranchSpacing) + transform.eulerAngles.z;
                float s = BulletBaseSpeed * (i % 2 * 2 - 1);
                Vector3 pos = transform.position;

                bulletData.colour = bulletData.gradient.Evaluate(i % 2 * 2 - 1);

                var bullet = SpawnBullet(2, z, pos, false);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }
        }

        Destroy();
    }
}