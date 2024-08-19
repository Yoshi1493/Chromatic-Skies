using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem6 : EnemyShooter<EnemyBullet>
{

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        //SetSubsystemEnabled(1);

        while (enabled)
        {
            //yield return WaitForSeconds(3f);

            SpawnProjectile(1, 0f, Vector3.zero).Fire();
            break;
        }
    }
}