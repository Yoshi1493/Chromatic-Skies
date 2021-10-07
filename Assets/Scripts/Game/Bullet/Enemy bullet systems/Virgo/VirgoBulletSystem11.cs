using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem11 : EnemyBulletSubsystem
{
    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);

        transform.Rotate(Random.Range(0f, 60f) * Vector3.forward);

        for (int i = 0; i < 18; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                float z = (i * 10f) + (j * 60f);
                SpawnBullet(2, z, transform.up.RotateVectorBy(z) / 2f).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown * 2f);
        }
    }
}