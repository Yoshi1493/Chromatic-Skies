using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem41 : EnemyShooter<Laser>
{
    //protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        while (enabled)
        {
            float z = Random.Range(0f, 360f);
            Vector3 pos = -transform.up.RotateVectorBy(z);

            SpawnProjectile(0, z, pos).Fire(1f);

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}