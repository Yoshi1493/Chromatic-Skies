using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int RepeatCount = 2;
    const int WaveCount = 12;
    const float WaveModifier = 0.8f;
    readonly float WaveRadius = Mathf.Pow(Mathf.PI, 1 / WaveModifier);
    const int BranchCount = 12;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletBaseSpeed = 2f;

    List<(Vector2 pos, float z)> bulletSpawnData = new(WaveCount * BranchCount);
    List<EnemyBullet> bullets = new(WaveCount * (BranchCount * 2 - 1));

    protected override float ShootingCooldown => 0.05f;

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float t = ii * BranchSpacing;
                    float x = Mathf.Lerp(0f, WaveRadius, i / (float)WaveCount);
                    float y = (iii % 2 * 2 - 1) * Mathf.Sin(Mathf.Pow(x, WaveModifier));

                    Vector3 pos = new Vector3(x, y).RotateVectorBy(t);
                    float z = (iii * 180f) - pos.GetRotationDifference(Vector3.zero);

                    bulletSpawnData.Add((pos, z));
                }
            }
        }
    }

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            for (int i = 0; i < RepeatCount; i++)
            {
                bullets.Clear();

                for (int ii = 1; ii < WaveCount; ii++)
                {
                    for (int iii = 0; iii < BranchCount; iii++)
                    {
                        for (int iv = 0; iv < BulletCount; iv++)
                        {
                            int b = (ii * BranchCount * BulletCount) + (iii * BulletCount) + iv;
                            (Vector3 pos, float z) = bulletSpawnData[b];
                            SpawnBullet(z, pos);
                        }
                    }

                    yield return WaitForSeconds(ShootingCooldown);
                }

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = ii * BranchSpacing + 90f;
                    Vector3 pos = WaveRadius * Vector3.down.RotateVectorBy(z);
                    SpawnBullet(z, pos);
                }

                yield return WaitForSeconds(1f);

                bullets.ForEach(b => b.Fire());
                yield return WaitForSeconds(2f);
            }

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(1f);
        }
    }

    void SpawnBullet(float z, Vector3 pos)
    {
        float s = BulletBaseSpeed * pos.magnitude;
        bulletData.colour = bulletData.gradient.Evaluate(s / 2f / WaveRadius);

        var bullet = SpawnProjectile(0, z, Vector3.zero);
        bullet.StartCoroutine(bullet.LerpSpeed(s, 0f, 1f));
        bullets.Add(bullet);
    }
}