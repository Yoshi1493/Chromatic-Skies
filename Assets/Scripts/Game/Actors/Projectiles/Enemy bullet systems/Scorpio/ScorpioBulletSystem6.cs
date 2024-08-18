using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem6 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 0;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        //while (enabled)
        //{
        //    yield return null;
        //}
    }
}