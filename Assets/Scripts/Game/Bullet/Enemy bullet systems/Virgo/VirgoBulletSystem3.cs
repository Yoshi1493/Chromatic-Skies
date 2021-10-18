using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem3 : EnemyShooter<EnemyBullet>
{
    readonly List<EnemyBullet> bullets = new List<EnemyBullet>();

    readonly float amp = 3f;
    readonly int n = 2;
    readonly int d = 7;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        //while (enabled)
        {
            //rhodonea curve r = cos(theta * n / d), where
            //theta = rotation amount on polar coordinates from point(1, 0)
            //n / d = angular frequency
            for (float i = 0; i < d; i += 0.04f)
            {
                float turnAngle = i * Mathf.PI;
                float r = Mathf.Sin(turnAngle * n / d);

                Vector3 spawnOffset = transform.right.RotateVectorBy(turnAngle * Mathf.Rad2Deg);
                float z = spawnOffset.GetRotationDifference(Vector3.zero);
                print($"{z:f1}");

                var b1 = SpawnProjectile(6, z, r * amp * spawnOffset);
                //var b2 = SpawnProjectile(6, z + 180, r * amp * -spawnOffset);

                //b1.StartCoroutine(b1.LerpSpeed(2f, 0f, 0.5f));
                //b2.StartCoroutine(b2.LerpSpeed(2f, 0f, 0.5f));

                bullets.Add(b1);
                //bullets.Add(b2);

                yield return WaitForSeconds(ShootingCooldown / 20f);
            }

            yield return WaitForSeconds(1f);

            bullets.ForEach(b => b.Fire());
            bullets.Clear();

            //yield return ownerShip.MoveToRandomPosition(1f, 1f);
        }
    }
}