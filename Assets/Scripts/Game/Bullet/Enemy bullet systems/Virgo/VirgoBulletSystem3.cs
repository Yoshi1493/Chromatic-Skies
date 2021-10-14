using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem3 : EnemyLaserSystem
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();
        SpawnLaser(0, 0f, Vector3.zero).Fire();
    }
}