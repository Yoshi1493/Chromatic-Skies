using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class LibraBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 10;
    const float SpawnMinAngle = 5f;
    const float SpawnMaxAngle = 15f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();
        
        SetSubsystemEnabled(1);

        while (enabled)
        {
            StartMoveAction?.Invoke();

            for (int i = 0; i < BulletCount; i++)
            {
                yield return WaitForSeconds(ShootingCooldown);

                float z = Random.Range(SpawnMinAngle, SpawnMaxAngle) * PositiveOrNegativeOne + 180f;
                Vector3 pos = Vector3.zero;
                SpawnProjectile(0, z, pos).Fire();
                SpawnProjectile(1, z, pos).Fire();
            }

            yield return WaitForSeconds(3f);

            SetSubsystemEnabled(2);
            yield return WaitForSeconds(10f);
        }
    }
}