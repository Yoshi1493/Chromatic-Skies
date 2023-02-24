using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int BulletRowCount = 5;
    const int BulletColCount = 5;
    const float BulletSpacing = 0.8f;

    protected override float ShootingCooldown => 0.04f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);
        SetSubsystemEnabled(2);

        while (enabled)
        {
            yield return WaitForSeconds(1f);

            for (int c = 0; c < BulletColCount; c++)
            {
                for (int r = 0; r < BulletRowCount; r++)
                {
                    float x = (r - ((BulletRowCount - 1) / 2f)) * BulletSpacing;
                    float y = (c - ((BulletColCount - 1) / 2f)) * BulletSpacing;
                    Vector3 pos = new(x, y);

                    float z = pos.GetRotationDifference(Vector3.zero);

                    var bullet = SpawnProjectile(0, z, Vector3.zero);
                    bullet.MoveSpeed = pos.magnitude * 2f;
                    bullet.Fire();

                    yield return WaitForSeconds(ShootingCooldown);
                }
            }

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(3f);
        }
    }
}