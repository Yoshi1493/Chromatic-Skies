using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem2 : EnemyBulletSystem
{
    List<EnemyBullet> bullets = new List<EnemyBullet>(19);

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return base.Shoot();

            for (int i = 0; i < 10; i++)
            {
                //sigmoid curve (3(i/a)^2 - 2(i/a)^3) * b, where
                //i = iterator
                //a = the point between [0.0, 1.0] at which the curve evaluates to b
                //b = local maximum height
                float xPos = (3 * Mathf.Pow((1 - (i * 0.1f)) / 0.8f, 2) - 2 * Mathf.Pow((1 - (i * 0.1f)) / 0.8f, 3)) * 0.3f;
                float yPos = i / -10f;

                var bulletR = SpawnBullet(0, 0f, 2 * new Vector3(xPos, yPos, 0f));
                bulletR.moveDirection = -Vector3.Reflect(bulletR.LookAt(2 * Vector3.down), Vector3.down);
                bullets.Add(bulletR);

                var bulletL = SpawnBullet(0, 0f, 2 * new Vector3(-xPos, yPos, 0f));
                bulletL.moveDirection = -Vector3.Reflect(bulletL.LookAt(2 * Vector3.down), Vector3.down);
                bullets.Add(bulletL);

                //bullets.Add(SpawnBullet(0, 0f, 2 * new Vector3(xPos, yPos, 0f)));
                //bullets.Add(SpawnBullet(0, 0f, 2 * new Vector3(-xPos, yPos, 0f)));

                yield return WaitForSeconds(ShootingCooldown / 4f);
            }

            bullets.Add(SpawnBullet(0, 0f, 2 * Vector3.down));

            yield return WaitForSeconds(1f);

            bullets.ForEach(b => b.Fire());

            //SetSubsystemEnabled(1, true);            

            yield return ownerShip.MoveToRandomPosition(1f, 5f);
        }
    }
}