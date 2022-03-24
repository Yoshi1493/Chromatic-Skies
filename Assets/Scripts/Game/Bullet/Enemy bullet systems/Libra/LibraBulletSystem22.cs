using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem22 : EnemyBulletSubsystem<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector3 randPos = new Vector2(camHalfWidth + 1f, Random.Range(-camHalfHeight, camHalfHeight));
                float z = Random.Range(-120f, -60f);

                SpawnProjectile(1, z, randPos, false).Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }
        }

    }
}