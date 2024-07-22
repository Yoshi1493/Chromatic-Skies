using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem51 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(ShootingCooldown);
    }
}