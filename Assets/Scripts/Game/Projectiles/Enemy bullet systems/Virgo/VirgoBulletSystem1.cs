using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem1 : EnemyShooter<EnemyBullet>
{
    readonly float BulletSpacing = (1f + Mathf.Sqrt(5f)) * 180f;

    protected override float ShootingCooldown => 1f / 60;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        StartMoveAction?.Invoke();
        SetSubsystemEnabled(1);

        for (int i = 0; enabled; i++)
        {
            float z = Mathf.Repeat(i * BulletSpacing, 360f);
            Vector3 pos = Vector3.zero;

            SpawnProjectile(0, z, pos).Fire();
            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}