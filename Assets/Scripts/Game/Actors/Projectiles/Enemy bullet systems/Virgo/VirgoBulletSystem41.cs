using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem41 : EnemyShooter<EnemyBullet>
{
    protected override float ShootingCooldown => 1.5f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);

        for (int i = 1; enabled; i *= -1)
        {
            float x = i * screenHalfWidth * Random.Range(0.5f, 0.8f);
            float y = screenHalfHeight * 1.1f;
            float z = 0f;

            Vector3 pos = new(x, y);
            SpawnProjectile(1, z, pos, false).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}