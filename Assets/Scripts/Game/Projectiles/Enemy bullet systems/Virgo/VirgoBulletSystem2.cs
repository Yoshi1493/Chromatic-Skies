using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class VirgoBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 2;
    const float RingRadius = 4.2f;
    const int RingCount = 12;
    const int BulletCount = 12;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletBaseSpeed = 2f;

    List<EnemyBullet> bullets = new(RingCount * (BulletCount * 2 - 1));

    protected override float ShootingCooldown => 1f / 60;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 1; ii < RingCount; ii++)
                {
                    Vector3 pos = Vector3.zero;
                    Vector3 newPos = Vector3.zero;

                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float t = iii * BulletSpacing;
                        float x = RingRadius * ii / RingCount;
                        float y = Mathf.Sin(Mathf.Pow(x, 0.8f));
                        newPos = new(x, y);

                        float z = (newPos - pos).GetRotationDifference(Vector3.zero) + t;
                        RotateVectorBy(ref newPos, t);

                        SpawnBullet(z, newPos);
                        newPos.y *= -1f;
                        SpawnBullet(-z, newPos);
                        newPos.y *= -1f;
                    }
                    pos = newPos;

                    yield return WaitForSeconds(ShootingCooldown);
                }

                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = ii * BulletSpacing;
                    Vector3 pos = RingRadius * Vector3.right.RotateVectorBy(z);
                    SpawnBullet(z + 90f, pos);
                }

                yield return WaitForSeconds(1f);

                bullets.ForEach(b => b.Fire());
                bullets.Clear();

                StartMoveAction?.Invoke();
                yield return WaitForSeconds(4f);
            }
        }
    }

    void SpawnBullet(float z, Vector3 pos)
    {
        float s = BulletBaseSpeed * pos.magnitude;
        bulletData.colour = bulletData.gradient.Evaluate(s / RingRadius * 0.5f);

        var bullet = SpawnProjectile(0, z, Vector3.zero);
        bullet.StartCoroutine(bullet.LerpSpeed(s, 0f, 1f));
        bullets.Add(bullet);
    }
}