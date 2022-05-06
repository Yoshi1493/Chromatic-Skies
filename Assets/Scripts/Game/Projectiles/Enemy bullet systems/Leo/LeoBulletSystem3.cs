using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem3 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);
        SetSubsystemEnabled(1);
        yield return WaitForSeconds(2f);
        SetSubsystemEnabled(2);
        yield return WaitForSeconds(2f);
        SetSubsystemEnabled(3);

        while (enabled)
        {
            float randDelay = Random.Range(4f, 6f);
            yield return WaitForSeconds(randDelay);
            yield return ownerShip.MoveToRandomPosition(1f);
        }
    }
}