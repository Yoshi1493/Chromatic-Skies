using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem21 : EnemyBulletSubsystem<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < 44; i++)
        {
            float rand = Random.Range(0f, 360f);

            for (int j = 0; j < 4; j++)
            {
                float z = rand + (j * 90f);
                SpawnProjectile(1, z, Vector3.zero).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}