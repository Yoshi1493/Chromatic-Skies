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
                float xPos = (3 * Mathf.Pow((1 - (i * 0.1f)) / 0.8f, 2) - 2 * Mathf.Pow((1 - (i * 0.1f)) / 0.8f, 3)) * 0.3f;
                float yPos = i / -10f;

                bullets.Add(SpawnBullet(0, 0f, 2 * new Vector3(xPos, yPos, 0f)));
                bullets.Add(SpawnBullet(0, 0f, 2 * new Vector3(-xPos, yPos, 0f)));

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