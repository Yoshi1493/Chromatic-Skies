using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 12;
    const float WaveModifier = 0.8f;
    readonly float WaveRadius = Mathf.Pow(Mathf.PI, 1 / WaveModifier);
    const int BranchCount = 12;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletBaseSpeed = 2f;

    List<Vector3> bulletSpawnData = new(WaveCount * BranchCount * BulletCount);
    List<EnemyBullet> bullets = new(WaveCount * BranchCount * BulletCount + WaveCount);

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
                    float y = (iii % 2 * -2 + 1) * Mathf.Sin(Mathf.Pow(x, WaveModifier));

                    Vector3 pos = new Vector3(x, y).RotateVectorBy(t);
                    float z = pos.GetRotationDifference(Vector3.zero);

                    bulletSpawnData.Add(new(pos.x, pos.y, z));
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
            bullets.Clear();

            for (int i = 1; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        int b = (i * BranchCount * BulletCount) + (ii * BulletCount) + iii;
                        Vector3 data = bulletSpawnData[b];

                        SpawnBullet(data.z, (Vector2)data);
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

            SetSubsystemEnabled(2);
            StartMoveAction?.Invoke();
            yield return WaitForSeconds(5f);
        }
    }

    void SpawnBullet(float z, Vector3 pos)
    {
        float s = BulletBaseSpeed * pos.magnitude;
        bulletData.colour = bulletData.gradient.Evaluate(s / 2f / WaveRadius);

        var bullet = SpawnProjectile(0, z, Vector3.zero);
        bullet.StartCoroutine(bullet.LerpSpeed(0f, s, 0.5f));
        bullet.StartCoroutine(bullet.LerpSpeed(s, 0f, 0.5f, delay: 0.5f));
        bullets.Add(bullet);
    }
}