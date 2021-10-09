using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem21 : EnemyBulletSubsystem
{
    Stack<EnemyBullet> bullets = new Stack<EnemyBullet>(80);

    readonly float a = 0.7f;
    readonly float b = 0.3f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < 10; i++)
        {
            //sigmoid curve (3(i/a)^2 - 2(i/a)^3) * b, where
            //i = iterator
            //a = the point between (0.0, 1.0] at which the curve evaluates to b
            //b = local maximum height
            float xPos = (3 * Mathf.Pow((1 - (i * 0.1f)) / a, 2) - 2 * Mathf.Pow((1 - (i * 0.1f)) / a, 3)) * b;
            float yPos = i / -10f;

            float zRot = (10 - i) * 5f;

            for (int j = 0; j < 360; j += 90)
            {
                var bulletR = SpawnBullet(5, zRot + j, 2 * new Vector3(xPos, yPos, 0f).RotateVectorBy(j));
                //bulletR.projectileData
                bullets.Push(bulletR);

                var bulletL = SpawnBullet(5, -zRot + j, 2 * new Vector3(-xPos, yPos, 0f).RotateVectorBy(j));
                bullets.Push(bulletL);
            }

            yield return WaitForSeconds(ShootingCooldown / 4f);
        }

        yield return WaitForSeconds(1f);

        int bulletCount = bullets.Count / 8;

        for (int i = 0; i < bulletCount; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                bullets.Pop().Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}