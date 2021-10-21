using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem1 : EnemyShooter<EnemyBullet>
{
    List<EnemyBullet> bigBullets = new List<EnemyBullet>();
    List<EnemyBullet> smallBullets = new List<EnemyBullet>();

    protected override IEnumerator Shoot()
    {
        //while (enabled)
        {
            yield return base.Shoot();

            bigBullets.Add(SpawnProjectile(1, 0f, new Vector3(6f, 5f), false));
            bigBullets.Add(SpawnProjectile(1, 180f, new Vector3(-6f, -5f), false));

            bigBullets.ForEach(b => b.Fire());

            for (int i = 0; i < 9; i++)
            {
                yield return WaitForSeconds(ShootingCooldown * 2f);

                smallBullets.Add(SpawnProjectile(2, 90f, bigBullets[0].transform.position, false));
                smallBullets.Add(SpawnProjectile(2, -90f, bigBullets[1].transform.position, false));
            }

            yield return WaitForSeconds(1.2f);

            for (int i = 0; i < smallBullets.Count; i += 2)
            {
                yield return WaitForSeconds(ShootingCooldown * 2f);

                for (int j = 0; j < 5; j++)
                {
                    SpawnProjectile(0, smallBullets[i].transform.localEulerAngles.z + (j * 72f), smallBullets[i].transform.position, false).Fire();
                    SpawnProjectile(0, smallBullets[i + 1].transform.localEulerAngles.z + (j * 72f), smallBullets[i + 1].transform.position, false).Fire();
                }
            }
        }
    }
}