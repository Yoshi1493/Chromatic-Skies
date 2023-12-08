using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem5 : EnemyShooter<EnemyBullet>
{

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            StartMoveAction?.Invoke();
            SetSubsystemEnabled(1);
            SetSubsystemEnabled(2);

            yield return WaitForSeconds(12f);
        }
    }
}