using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem3 : EnemyShooter<EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            float rand = Random.Range(0f, 22.5f);

            for (int i = 1; i < 24; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    float z = (j * 22.5f) + (i * 8f) + rand;
                    bulletData.colour = bulletData.gradient.Evaluate(i / 23f);

                    var bullet = SpawnProjectile(0, z, (i / 1.5f) * transform.up.RotateVectorBy(z));
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown * 2f);
            }

            yield return ownerShip.MoveToRandomPosition(2f, delay: 12f);
        }
    }
}