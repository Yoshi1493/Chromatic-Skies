using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem21 : EnemyBulletSubsystem<EnemyBullet>
{
    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = 45; i > -45; i--)
            {
                Vector3 randPos = new Vector2(Random.Range(-camHalfWidth, camHalfWidth), camHalfHeight + 1f);
                SpawnProjectile(0, i, randPos).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }

            for (int i = -45; i < 45; i++)
            {
                Vector3 randPos = new Vector2(Random.Range(-camHalfWidth, camHalfWidth), camHalfHeight + 1f);
                SpawnProjectile(0, i, randPos).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }
        }

    }
}