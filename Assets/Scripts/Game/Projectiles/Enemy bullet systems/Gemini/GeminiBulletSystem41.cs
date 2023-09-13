using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBulletSystem41 : EnemyShooter<EnemyBullet>
{
    const float ArcHalfWidth = 60f;
    const float BulletSpacing = 10f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        int r = Random.Range(0, 360 / (int)BulletSpacing);

        for (int i = r; enabled; i++)
        {
            float t = Mathf.Sin(i * BulletSpacing * Mathf.Deg2Rad);
            float z = t * ArcHalfWidth;
            Vector3 pos = transform.up.RotateVectorBy(z);

            SpawnProjectile(2, z, pos).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}