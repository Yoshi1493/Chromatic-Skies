using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem62 : EnemyShooter<EnemyBullet>
{
    const int RepeatCount = 2;
    const int ParentBulletCount = 5;
    const float ParentBulletSpacing = 360f / ParentBulletCount;
    const int WaveCount = 3;
    const int ChildBulletCount = 4;
    const float ChildBulletSpacing = 360f / ChildBulletCount;
    const float BulletRotationSpeed = 60f;
    const float BulletRotationSpeedModifier = 20f;
    const float BulletRotationDuration = 3f;

    List<EnemyBullet> bullets = new(ParentBulletCount);
    List<EnemyBullet> childBullets = new(ParentBulletCount * (int)Mathf.Pow(ChildBulletCount, WaveCount));

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        //enabled = false;
        //yield break;

        bullets.Clear();

        for (int i = 0; i < ParentBulletCount; i++)
        {
            float z = i * ParentBulletSpacing;
            Vector3 pos = Vector3.zero;

            var bullet = SpawnProjectile(3, z, pos);
            bullet.Fire();
            bullets.Add(bullet);
        }

        yield return WaitForSeconds(ShootingCooldown);

        for (int i = 0; i < RepeatCount; i++)
        {
            childBullets.Clear();

            bulletData.colour = bulletData.gradient.Evaluate(0f);

            int d = i % 2 * 2 - 1;

            for (int ii = 0; ii < bullets.Count; ii++)
            {
                Transform b = bullets[ii].transform;

                for (int iii = 0; iii < ChildBulletCount; iii++)
                {
                    float z = (iii * ChildBulletSpacing) + b.eulerAngles.z;
                    Vector3 pos = b.position;

                    var bullet = SpawnProjectile(4, z, pos, false);
                    bullet.StartCoroutine(bullet.RotateBy(d * BulletRotationSpeed, 1f));
                    bullet.StartCoroutine(bullet.RotateBy(-d * BulletRotationSpeed, BulletRotationDuration, delay: ShootingCooldown * (WaveCount - 1f)));
                    bullet.Fire();
                    childBullets.Add(bullet);
                }
            }

            yield return WaitForSeconds(ShootingCooldown);

            for (int ii = 1; ii < WaveCount; ii++)
            {
                d *= -1;

                bulletData.colour = bulletData.gradient.Evaluate(ii / (WaveCount - 1f));

                List<EnemyBullet> bs = new();

                for (int iii = 0; iii < ParentBulletCount * (int)Mathf.Pow(ChildBulletCount, ii); iii++)
                {
                    Transform t = childBullets[iii].transform;

                    for (int iv = 0; iv < ChildBulletCount; iv++)
                    {
                        float z = (iv * ChildBulletSpacing) + t.eulerAngles.z;
                        float s = BulletRotationSpeed + (iv * BulletRotationSpeedModifier);
                        Vector3 pos = t.position;

                        var bullet = SpawnProjectile(4, z, pos, false);
                        bullet.StartCoroutine(bullet.RotateBy(d * s, 1f));
                        bullet.StartCoroutine(bullet.RotateBy(-d * s, BulletRotationDuration, delay: ShootingCooldown * (WaveCount - 1f)));
                        bullet.Fire();
                        bs.Add(bullet);
                    }
                }

                childBullets.InsertRange(0, bs);
                bs.Clear();

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(2f * ShootingCooldown);
        }

        enabled = false;
    }
}