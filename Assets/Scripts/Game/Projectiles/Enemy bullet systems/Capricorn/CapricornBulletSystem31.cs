using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const int BranchCount = 7;
    const float BranchSpacing = 15f;
    const int MinBulletCount = 1;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.4f;

    protected override float ShootingCooldown => 0.3f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            yield return WaitForSeconds(ShootingCooldown);

            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int ii = 0; ii < BranchCount; ii++)
            {
                int bulletCount = MinBulletCount + ((int)Mathf.PingPong(ii, BranchCount / 2) * 2);

                for (int iii = 0; iii < bulletCount; iii++)
                {
                    float z = ((ii - ((BranchCount - 1) / 2f)) * BranchSpacing) + r;
                    float s = BulletBaseSpeed + (iii * BulletSpeedModifier);
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                    var bullet = SpawnProjectile(1, z, pos);
                    bullet.MoveSpeed = s;
                    bullet.StartCoroutine(bullet.LerpSpeed(BulletBaseSpeed, s, 1f));
                }
            }
        }

        enabled = false;
    }
}