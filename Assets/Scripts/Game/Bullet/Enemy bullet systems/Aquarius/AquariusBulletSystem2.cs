using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem2 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            yield return null;
        }
    }
}