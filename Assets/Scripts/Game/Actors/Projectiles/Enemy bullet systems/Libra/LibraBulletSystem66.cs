using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem66 : EnemyShooter<EnemyBullet>
{
    const int ParentBulletCount = 6;
    const float ParentBulletSpacing = 360f / ParentBulletCount;
    const int WaveCount = 2;
    const float WaveSpacing = ParentBulletSpacing * 0.5f;
    const int BulletCount = 60;
    const float BulletSpacing = 20f;
    const float BulletBaseSpeed = 1.2f;
    const float BulletSpeedModifier = 0.02f;

    List<EnemyBullet> bullets = new(ParentBulletCount);

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        //enabled = false;
        //yield break;

        for (int i = 0; i < WaveCount; i++)
        {
            bullets.Clear();

            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int ii = 0; ii < ParentBulletCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * ParentBulletSpacing) + r;
                Vector3 pos = Vector3.zero;

                var bullet = SpawnProjectile(11, z, pos);
                bullet.Fire();
                bullets.Add(bullet);
            }

            yield return WaitForSeconds(1f);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                for (int iii = 0; iii < ParentBulletCount; iii++)
                {
                    Transform b = bullets[iii].transform;

                    float z = (ii * BulletSpacing) + b.eulerAngles.z;
                    float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                    Vector3 pos = b.position;

                    bulletData.colour = bulletData.gradient.Evaluate(ii / (BulletCount - 1f));

                    var bullet = SpawnProjectile(12, z, pos, false);
                    bullet.StartCoroutine(bullet.LerpSpeed(0f, s, 1f));
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(2f);
        }

        enabled = false;
    }
}