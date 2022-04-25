using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem41 : EnemyBulletSubsystem<Laser>
{
    const int WaveCount = 3;
    const int WaveSpacing = 360 / WaveCount;
    const int LaserCount = 6;
    const int LaserSpacing = 360 / LaserCount;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2.5f);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                float randOffset = Random.Range(0f, LaserSpacing);

                yield return WaitForSeconds(1f);

                for (int j = 0; j < LaserCount; j++)
                {
                    float z = (j * LaserSpacing) + randOffset;
                    SpawnProjectile(0, z, Vector3.zero).Fire();
                }
            }

            enabled = false;
        }
    }
}