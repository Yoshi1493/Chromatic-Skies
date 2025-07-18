using System.Collections;
using static CoroutineHelper;

public class TaurusBossShooter3 : BossShooter<BossBullet>
{
    protected override float ShootingCooldown => 4f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);
        SetSubsystemEnabled(2);

        while (enabled)
        {
            yield return WaitForSeconds(ShootingCooldown);
            StartMoveAction?.Invoke();
        }
    }
}