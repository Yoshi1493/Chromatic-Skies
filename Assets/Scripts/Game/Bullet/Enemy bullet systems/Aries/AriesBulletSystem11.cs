using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem11 : EnemyShooter<EnemyBullet>
{
    List<EnemyBullet> bigBullets = new List<EnemyBullet>();
    List<EnemyBullet> smallBullets = new List<EnemyBullet>();

    protected override IEnumerator Shoot()
    {
        yield return null;
    }
}