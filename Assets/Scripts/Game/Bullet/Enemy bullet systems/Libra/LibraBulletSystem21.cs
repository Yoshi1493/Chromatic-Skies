using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem21 : EnemyBulletSubsystem<Laser>
{
    const float MinLaserSpacing = 1f;
    const float MaxLaserSpacing = 3f;
    const float MinLaserRotation = 5f;
    const float MaxLaserRotation = 30f;

    List<float> laserPositions = new List<float>();

    protected override float ShootingCooldown => 0.04f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return WaitForSeconds(2f);

            float x = -camHalfWidth;

            while (x < camHalfWidth)
            {
                x += Random.Range(MinLaserSpacing, MaxLaserSpacing);
                laserPositions.Add(x);
            }

            for (int i = 0; i < laserPositions.Count - 1; i++)
            {
                int r = Random.Range(i, laserPositions.Count);
                var temp = laserPositions[i];
                laserPositions[i] = laserPositions[r];
                laserPositions[r] = temp;
            }

            for (int i = 0; i < laserPositions.Count; i++)
            {
                float y = camHalfHeight;
                float z = Random.Range(MinLaserRotation, MaxLaserRotation) * Mathf.Sign(Random.value - 0.5f) + 180f;

                SpawnProjectile(0, z, new Vector3(laserPositions[i], y), false).Fire(1f);

                yield return WaitForSeconds(ShootingCooldown);
            }

            laserPositions.Clear();

            yield return WaitForSeconds(2f);
        }
    }
}