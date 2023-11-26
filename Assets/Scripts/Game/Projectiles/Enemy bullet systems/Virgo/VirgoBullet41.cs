using System.Collections;
using UnityEngine;

public class VirgoBullet41 : ScriptableEnemyBullet<VirgoBulletSystem41, EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;

    const int BranchCount = 18;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletStartSpeed = 4f;
    const float BulletEndSpeed = 1.5f;
    const float BulletRotationAmount = 60f;
    const float BulletRotationDuration = 5f;
    const float ShootingCooldown = 1.5f;

    protected override float MaxLifetime => 4f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(Random.Range(2f, 5f), 0f, MaxLifetime);
    }

    public override void Destroy()
    {
        for (int i = 0; i < BranchCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = i * BranchSpacing;
                Vector3 pos = transform.position;

                bulletData.colour = bulletData.gradient.Evaluate(ii);

                var bullet = SpawnBullet(2, z, pos, false);
                bullet.StartCoroutine(bullet.LerpSpeed(BulletStartSpeed, BulletEndSpeed, 2f));
                bullet.StartCoroutine(bullet.RotateBy((ii % 2 * 2 - 1) * BulletRotationAmount, BulletRotationDuration));
            }
        }

        base.Destroy();
    }
}