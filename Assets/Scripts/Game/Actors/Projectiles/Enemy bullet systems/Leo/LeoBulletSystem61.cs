using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class LeoBulletSystem61 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 36;
    const float WaveSpacing = 360f / WaveCount;
    const int BranchCount = 9;
    const float BulletSpawnRadius = 1.5f;
    const float BulletBaseSpeed = 2.5f;
    const float BulletRotationSpeed = -180f;
    const float BulletRotationSpeedModifier = 10f;
    const float BulletRotationDuration = 3f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            float r = PositiveOrNegativeOne * (Random.Range(0f, 15f) + 15f);
            Vector3 v = BulletSpawnRadius * Vector3.up.RotateVectorBy(r);

            for (int i = 0; i < WaveCount; i++)
            {
                float a = i * WaveSpacing;
                Vector3 rotationAxis = Quaternion.AngleAxis(a, Vector3.up) * v;
                //print(rotationAxis);

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = r + 90f;
                    float y = Mathf.Lerp(BulletSpawnRadius, -BulletSpawnRadius, ii / (BranchCount - 1f));
                    float x = Mathf.Sqrt((BulletSpawnRadius * BulletSpawnRadius) - (y * y));
                    //Vector3 pos = new Vector3(x, y).RotateVectorBy(Mathf.LerpUnclamped(0f, r, Mathf.Lerp(-1, 1, (float)i / WaveCount)));
                    Vector3 pos = BulletSpawnRadius * new Vector3(x, y).RotateVectorBy(r);

                    var bullet = SpawnProjectile(1, z, pos) as LeoBullet61;
                    bullet.MoveSpeed = BulletSpawnRadius;
                    bullet.rotationAxis = v;
                    //bullet.StartCoroutine(bullet.LerpSpeed(BulletSpawnRadius, BulletBaseSpeed, 1f, delay: LeoBullet61.FireDelay));
                    //bullet.StartCoroutine(bullet.RotateBy(BulletRotationSpeed + (i * BulletRotationSpeedModifier), BulletRotationDuration, delay: LeoBullet61.FireDelay));
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(30f);
        }
    }
}