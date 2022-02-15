using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem1 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < 66; i++)
            {
                SpawnProjectile(0, Random.Range(0f, 360f), Vector3.zero).Fire();
                SpawnProjectile(0, Random.Range(0f, 360f), Vector3.zero).Fire();

                yield return WaitForSeconds(ShootingCooldown / 2f);
            }

            yield return WaitForSeconds(ShootingCooldown);

            for (int i = 0; i < 6; i++)
            {
                float alt = ((i % 2) - 0.5f) * 2;
                float offset = 10f * alt;

                for (int j = 0; j < 6; j++)
                {
                    for (int k = 0; k < 6; k++)
                    {
                        float z = (offset * (j - 1)) + (k * 60f) + (2.5f * alt);
                        SpawnProjectile(1, z, Vector3.zero).Fire();
                    }

                    yield return WaitForSeconds(ShootingCooldown / 2f);
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);
            StartCoroutine(ownerShip.MoveToRandomPosition(1f));

            for (int i = 0; i < 3; i++)
            {
                float z = transform.position.GetRotationDifference(PlayerPosition) + 180f;

                for (int j = 0; j < 6; j++)
                {
                    var bullet = SpawnProjectile(2, z, Vector3.zero);
                    bullet.GetComponent<DefaultEnemyBullet>().speeds.z = j + 2f;
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown * 3f);
            }

            yield return WaitForSeconds(1f);
        }
    }
}