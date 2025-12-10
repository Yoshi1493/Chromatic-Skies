using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiShooter02 : CommonEnemyShooter<EnemyBullet>
{
    const int RepeatCount = 2;
    const int WaveCount = 32;
    const float WaveSpacing = 16f;
    const float WaveAmplitude = 1.2f;
    const float BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletRotationSpeed = 90f;
    const float BulletRotationSpeedModifier = 15f;
    const float BulletRotationDuration = 3f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        for (int i = 0; i < RepeatCount; i++)
        {
            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    float z = ii * WaveSpacing;
                    float x = z * Mathf.Deg2Rad;
                    float y = (iii % 2 * 2 - 1) * WaveAmplitude * Mathf.Sin(x);
                    Vector3 pos = new Vector3(y, -x).RotateVectorBy(r);

                    bulletData.colour = bulletData.gradient.Evaluate(ii % 2);

                    var bullet = SpawnProjectile(0, z, pos);
                    bullet.StartCoroutine(bullet.RotateBy(Mathf.PingPong(ii * BulletRotationSpeedModifier, BulletRotationSpeed), BulletRotationDuration, delay: 1.5f));
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);
        }
    }
}