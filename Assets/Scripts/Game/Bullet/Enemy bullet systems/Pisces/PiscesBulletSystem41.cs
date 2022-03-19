using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem41 : EnemyBulletSubsystem<Laser>
{
    const int WaveCount = 3;
    const int LaserCount = 6;
    const int Spacing = 360 / LaserCount;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2.5f);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int j = 0; j < LaserCount; j++)
                {
                    //jx + 0.5ix
                    float z = Spacing * 0.5f * (2 * j + i);

                    SpawnProjectile(0, z - 5f, Vector3.zero);
                    SpawnProjectile(0, z + 5f, Vector3.zero);
                }

                yield return WaitForSeconds(1f);
            }

            enabled = false;
        }
    }
}