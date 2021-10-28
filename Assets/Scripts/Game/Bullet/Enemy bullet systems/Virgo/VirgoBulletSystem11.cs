using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem11 : EnemyBulletSubsystem<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);

        transform.localEulerAngles = new Vector3(0f, 0f, Random.Range(0f, 60f));

        for (int i = 0; i < 18; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                float z = (i * 10f) + (j * 60f);
                SpawnProjectile(1, z, transform.up.RotateVectorBy(z) / 2f).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown * 2f);
        }

        enabled = false;
    }
}