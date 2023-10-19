using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem6 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 24;
    const float WaveSpacing = 4f;
    const int BranchCount = 18;
    const float BranchSpacing = 360f / BranchCount;
    const float SpawnRadiusMultiplier = 0.5f;

    List<EnemyBullet> bullets = new(WaveCount * BranchCount);
    List<(Vector2 pos, float z)> bulletPositions = new(WaveCount * BranchCount);

    protected override float ShootingCooldown => 1 / 60f;

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BranchSpacing);
                Vector3 pos = (i * SpawnRadiusMultiplier) * transform.up.RotateVectorBy(z);

                bulletPositions.Add((pos, z));
            }
        }
    }

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            StartMoveAction?.Invoke();
            yield return WaitForSeconds(13f);

            Vector3 playerPos = PlayerPosition;

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    int b = (i * BranchCount) + ii;

                    if (!(bulletPositions[b].pos + (Vector2)ownerShip.transform.position).IsTooClose(playerPos))
                    {
                        float z = bulletPositions[b].z;
                        Vector3 pos = bulletPositions[b].pos;

                        bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                        var bullet = SpawnProjectile(0, z, pos) as CapricornBullet60;
                        bullet.DestroyAction += OnSpawnedBulletDestroy;
                        bullets.Add(bullet);
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(0.5f);
            SetSubsystemEnabled(1);

            bullets.ForEach(b => b.Fire());
            bullets.Clear(); 

        }
    }

    void OnSpawnedBulletDestroy(EnemyBullet bullet)
    {
        bullets.Remove(bullet);
    }
}