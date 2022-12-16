using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int MaxBullets = 5;
    const int MaxAngle = 15;

    protected override float ShootingCooldown => 0.12f;

    protected override IEnumerator Shoot()
    {
        int r = Random.value > 0.5f ? 0 : MaxAngle;
        float y = screenHalfHeight + 1f;

        while (enabled)
        {
            float z = Mathf.PingPong(r, MaxAngle) - (MaxAngle * 0.5f);
            float x = Random.Range(-screenHalfWidth, screenHalfWidth);
            Vector3 pos = new(x, y);

            SpawnProjectile(1, z, pos, false).Fire();
            yield return WaitForSeconds(ShootingCooldown);

            r++;
        }
    }
}