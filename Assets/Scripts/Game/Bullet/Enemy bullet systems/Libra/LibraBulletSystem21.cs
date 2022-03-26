using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem21 : EnemyBulletSubsystem<EnemyBullet>
{
    protected override float ShootingCooldown => 0.04f;
    readonly float goldenRatio = (1f + Mathf.Sqrt(5f)) * 180f;

    protected override IEnumerator Shoot()
    {
        float randDirection = Mathf.Sign(Random.value - 0.5f);
        int i = 0;

        while (enabled)
        {
            float z = i * goldenRatio;
            SpawnProjectile(0, z * randDirection, Vector3.zero).Fire();

            i++;
            yield return WaitForSeconds(ShootingCooldown);
        }

    }
}