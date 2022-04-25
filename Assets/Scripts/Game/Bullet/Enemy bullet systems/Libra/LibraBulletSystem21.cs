using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem21 : EnemyBulletSubsystem<EnemyBullet>
{
    readonly float goldenRatio = (1f + Mathf.Sqrt(5f)) * 180f;

    protected override float ShootingCooldown => 0.04f;

    protected override IEnumerator Shoot()
    {
        float randDirection = Mathf.Sign(Random.value - 0.5f);
        int i = 0;

        while (enabled)
        {
            float z = i * goldenRatio * randDirection;
            SpawnProjectile(0, z , transform.up.RotateVectorBy(z) * 0.5f).Fire();

            i++;
            yield return WaitForSeconds(ShootingCooldown);
        }

    }
}