using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const float WaveSpacing = 222f;
    const int BulletCount = 7;
    const float BulletSpacing = 4f;
    const float BulletBaseSpeed = 4f;
    const float BulletSpeedMultiplier = 0.5f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        StartMoveAction?.Invoke();
        SetSubsystemEnabled(1);
        yield return WaitForSeconds(3f);

        int i = 0;

        while (enabled)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float t = ii - ((BulletCount - 1) / 2f);
                float z = (i * WaveSpacing) + (t * BulletSpacing);
                float s = BulletBaseSpeed - (Mathf.Abs(t) * BulletSpeedMultiplier * BulletSpeedMultiplier);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(ii / (BulletCount - 1f));

                var bullet = SpawnProjectile(0, z, pos);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
            i++;
        }
    }
}