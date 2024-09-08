using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 3;

    protected override float ShootingCooldown => 0.4f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        StartMoveAction?.Invoke();
        SetSubsystemEnabled(1);

        while (enabled)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = 0f;
                float x = transform.position.x + Random.Range(-3f, 3f);
                float y = screenHalfHeight * 1.1f;
                Vector3 pos = new(x, y);

                SpawnProjectile(0, z, pos, false).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}