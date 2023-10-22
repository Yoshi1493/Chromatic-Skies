using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBullet61 : ScriptableEnemyBullet<CapricornBulletSystem6, EnemyBullet>
{
    const int WaveCount = 40;
    const float WaveSpacing = 360f / BranchCount / WaveCount;
    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;
    const float ShootingCooldown = 3f / 60;
    const float BulletBaseSpeed = 3f;

    [SerializeField] ProjectileObject bulletData;

    protected override IEnumerator Move()
    {
        StartCoroutine(this.LerpSpeed(3f, 1f, 2f));
        StartCoroutine(this.RotateBy(270f, 2f));

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

            yield return WaitForSeconds(ShootingCooldown);
        }

        Destroy();
    }
}