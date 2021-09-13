using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem41 : EnemyBulletSubsystem
{
    protected override IEnumerator Shoot()
    {
        yield return null;

        while (enabled)
        {
            for (int i = 0; i < 2; i++)
            {
                float z = Random.Range(-7, 7);

                while (z == 0)
                    z = Random.Range(-7, 7);

                SpawnBullet(6, -45f * Mathf.Sign(z), z * Vector2.right);
            }
            yield return WaitForSeconds(ShootingCooldown * 5f);
        }
    }
}