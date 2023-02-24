using System.Collections;
using static CoroutineHelper;

public class LeoBulletSystem3 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);
        yield return WaitForSeconds(2f);

        SetSubsystemEnabled(2);
        yield return WaitForSeconds(2f);

        SetSubsystemEnabled(3);
        yield return WaitForSeconds(1f);

        SetSubsystemEnabled(4);
        StartMoveAction?.Invoke();
    }
}