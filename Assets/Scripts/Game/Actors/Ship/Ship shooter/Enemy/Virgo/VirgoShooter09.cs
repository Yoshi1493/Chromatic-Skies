using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoShooter09 : CommonEnemyShooter<EnemyBullet>
{
    const int BranchCount = 5;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 16;
    const float BulletSpacing = 360f / BranchCount / BulletCount;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        for (int i = 0; i < BranchCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (i * BranchSpacing) + (ii * BulletSpacing);
                float t = ii / (BulletCount - 1f);
                float r = Mathf.Lerp(-1f, 1f, t) * BranchSpacing * 2f;
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(t);

                var bullet = SpawnProjectile(0, z, pos);
                bullet.StartCoroutine(bullet.RotateBy(r, 1f, delay: 1f));
                bullet.Fire();
            }
        }
    }
}