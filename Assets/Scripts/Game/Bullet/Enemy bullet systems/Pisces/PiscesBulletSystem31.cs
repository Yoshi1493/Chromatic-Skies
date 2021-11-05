using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem31 : EnemyBulletSubsystem<Laser>
{
    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(5f);

        for (int i = 0; i < 33; i++)
        {
            float r = Random.Range(0, 120f);

            for (int j = 0; j < 3; j++)
            {
                float z = r + (j * 120f);
                SpawnProjectile(0, z, 0.5f * transform.up.RotateVectorBy(z + 90));
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}