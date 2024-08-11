using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem65 : EnemyShooter<EnemyBullet>
{
    const int ParentBulletCount = 2;
    const float ParentBulletBaseSpeed = 2f;
    const float ParentBulletSpeedModifier = 1.5f;
    const float ParentBulletRotationSpeed = 90f;
    const float ParentBulletRotationSpeedModifier = 45f;
    const int WaveCount = 8;
    const int BranchCount = 16;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 5;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.4f;
    const float BulletRotationSpeed = 30f;
    const float BulletRotationSpeedModifier = 10f;

    List<EnemyBullet> bullets = new(ParentBulletCount);

    protected override float ShootingCooldown => 1.5f;

    protected override IEnumerator Shoot()
    {
        //    enabled = false;
        //    yield break;

        bullets.Clear();

        for (int i = 0; i < ParentBulletCount; i++)
        {
            Vector3 v1 = ownerShip.transform.position;
            float z = 0f;
            float r = (i % 2 * 2 - 1) * (ParentBulletRotationSpeed + (i * ParentBulletRotationSpeedModifier));
            float s = ParentBulletBaseSpeed + (i * ParentBulletSpeedModifier);
            Vector3 pos = Vector3.zero;

            var bullet = SpawnProjectile(9, z, pos);
            bullet.StartCoroutine(bullet.LerpSpeed(s, 0f, 1f));
            bullet.StartCoroutine(bullet.TransformRotateAround(v1, 10f, r, delay: 1f));
            bullets.Add(bullet);
        }

        yield return WaitForSeconds(1f);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < ParentBulletCount; ii++)
            {
                Transform b = bullets[ii].transform;

                for (int iii = 0; iii < BranchCount; iii++)
                {
                    for (int iv = 0; iv < BulletCount; iv++)
                    {
                        float z = (iii * BranchSpacing) + b.eulerAngles.z;
                        float r = (i % 2 * 2 - 1) * (BulletRotationSpeed + ((i + iv) * BulletRotationSpeedModifier));
                        float s = BulletBaseSpeed + (iv * BulletSpeedModifier);
                        Vector3 pos = b.position;

                        bulletData.colour = bulletData.gradient.Evaluate(iv / (BulletCount - 1f));

                        var bullet = SpawnProjectile(10, z, pos, false);
                        bullet.StartCoroutine(bullet.RotateBy(r, 2f, delay: 1f));
                        bullet.StartCoroutine(bullet.LerpSpeed(0f, s, 1f, delay: 1f));
                        bullet.Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown / ParentBulletCount);
            }
        }

        enabled = false;
    }
}