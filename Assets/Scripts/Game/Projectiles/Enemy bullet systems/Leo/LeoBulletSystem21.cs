using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem21 : EnemyBulletSubsystem<EnemyBullet>
{
    const int BranchCount = 16;
    const float BranchSpacing = 360f / BranchCount;
    public const float BulletSpeed = 4f;

    protected override float ShootingCooldown => 5f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return WaitForSeconds(1f);

            float r = transform.eulerAngles.z;

            for (int i = 0; i < BranchCount; i++)
            {
                float z = i * BranchSpacing;

                var bullet = SpawnProjectile(2, r, Vector3.zero);
                bullet.moveDirection *= (i + BranchCount / 4 - 2) / (BranchCount / 2 - 1) % 2 * 2 - 1;
                bullet.MoveSpeed = Mathf.Sin(z * Mathf.Deg2Rad) * BulletSpeed;

                bullet.Fire();
            }

            enabled = false;
        }
    }
}