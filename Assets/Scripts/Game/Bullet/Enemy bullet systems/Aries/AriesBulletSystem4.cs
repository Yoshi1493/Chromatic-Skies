using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem4 : EnemyBulletSystem
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        EnableSubsystem(1);

        while (enabled)
        {
            yield return WaitForSeconds(2f);

            float n = 0f;
            float randSpinDirection = Random.value - 0.5f;
            float randStartAngle = Random.Range(0f, 90f);

            for (int i = 0; i < 360; i += 4)
            {
                for (int j = 0; j < 5; j++)
                {
                    float z = (j * 72f) + n + randStartAngle;

                    SpawnBullet(0, z, Vector2.zero);

                }

                n += i * Mathf.Sign(randSpinDirection);
                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return ownerShip.MoveToRandomPosition(1f, 1f);
        }
    }
}