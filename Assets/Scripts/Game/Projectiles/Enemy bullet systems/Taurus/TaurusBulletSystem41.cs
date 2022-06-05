using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem41 : EnemyShooter<Laser>
{
    const int LaserCount = 19;
    const float LaserSpacing = 1.2f;
    const float MaxRotation = 20f;

    protected override float ShootingCooldown => 0.04f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            transform.localEulerAngles = Random.Range(-MaxRotation, MaxRotation) * Vector3.forward;
            float z = transform.eulerAngles.z;
            float sinZ = Mathf.Sin(transform.localEulerAngles.z * Mathf.Deg2Rad);

            for (int i = 0; i < LaserCount; i++)
            {
                float offset = ((LaserCount - 1) * -0.5f) + i;
                float x = offset * LaserSpacing;
                float y = offset * sinZ;
                Vector3 spawnPos = new(x, y);

                SpawnProjectile(0, z, spawnPos).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }

            enabled = false;
        }
    }
}