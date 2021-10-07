using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem21 : EnemyBulletSubsystem
{
    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);
    }
}