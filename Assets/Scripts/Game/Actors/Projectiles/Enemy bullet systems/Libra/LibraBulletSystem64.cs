using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem64 : EnemyShooter<EnemyBullet>
{
    const int ParentBulletCount = 4;
    const float ParentBulletSpacing = 360f / ParentBulletCount;
    const int RepeatCount = 2;
    const int WaveCount = 24;
    const float WaveSpacing = 10f;
    const int BranchCount = 3;
    const float BranchSpacing = 360f / BranchCount;
    const int ChildBulletCount = 2;
    const float BulletBaseSpeed = 1f;
    const float BulletSpeedModifier = 0.05f;
    const float BulletRotationSpeed = 60f;

    List<EnemyBullet> bullets = new(ParentBulletCount);

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        enabled = false;
        yield break;

        bullets.Clear();

        for (int i = 0; i < ParentBulletCount; i++)
        {
            float z = i * ParentBulletSpacing;
            Vector3 pos = Vector3.zero;

            var bullet = SpawnProjectile(7, z, pos);
            bullet.Fire();
            bullets.Add(bullet);
        }

        yield return WaitForSeconds(1f);

        for (int i = 0; i < RepeatCount; i++)
        {
            float r = Random.Range(0f, ParentBulletSpacing);

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < ParentBulletCount; iii++)
                {
                    Transform b = bullets[iii].transform;

                    for (int iv = 0; iv < BranchCount; iv++)
                    {
                        for (int v = 0; v < ChildBulletCount; v++)
                        {
                            float z = (ii * WaveSpacing) + (iv * BranchSpacing) + r;
                            Vector3 pos = b.position;

                            var bullet = SpawnProjectile(8, z, pos, false);
                            bullet.StartCoroutine(bullet.RotateBy((v % 2 * 2 - 1) * BulletRotationSpeed, 2f));
                            bullet.StartCoroutine(bullet.RotateBy((ii % 2 * 2 - 1) * BulletRotationSpeed, 0f, delay: 2.1f));
                            bullet.Fire();
                        }
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(5f - (WaveCount * ShootingCooldown));
        }

        enabled = false;
    }
}