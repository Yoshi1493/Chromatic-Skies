using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem51 : EnemyShooter<EnemyBullet>
{
    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(ShootingCooldown);
    }
}