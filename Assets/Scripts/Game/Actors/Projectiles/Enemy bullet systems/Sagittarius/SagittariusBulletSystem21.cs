using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const float WaveSpacing = 222f;
    const int BulletCount = 7;
    const float BulletSpacing = 4f;
    const float BulletBaseSpeed = 4f;
    const float BulletSpeedModifier = 0.5f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);

        int i = 0;

        while (enabled)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float t = ii - ((BulletCount - 1) / 2f);
                float z = (i * WaveSpacing) + (t * BulletSpacing);
                float s = BulletBaseSpeed - (Mathf.Abs(t) * BulletSpeedModifier * BulletSpeedModifier);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(ii / (BulletCount - 1f));

                var bullet = SpawnProjectile(1, z, pos);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
            i++;
        }
    }
}