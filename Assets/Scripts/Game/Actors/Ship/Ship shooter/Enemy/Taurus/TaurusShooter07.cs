using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusShooter07 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const int BranchCount = 12;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletRotationSpeed = 90f;
    const float BulletRotationDuration = 6f;

    TaurusShooter05 parentShooter;

    protected override float ShootingCooldown => 1.0f;

    protected override void Awake()
    {
        base.Awake();

        parentShooter = transform.parent.GetComponentInChildren<TaurusShooter05>();
        parentShooter.ShootAction += OnParentShooterShoot;
    }

    void OnParentShooterShoot(float angle)
    {
        print("shoot called.");
    }

    protected override IEnumerator Shoot()
    {
        yield return null;
    }

    void OnDestroy()
    {
        parentShooter.ShootAction -= OnParentShooterShoot;
    }
}