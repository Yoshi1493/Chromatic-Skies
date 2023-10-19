using System.Collections;
using UnityEngine;

public class CapricornBullet61 : ScriptableEnemyBullet<CapricornBulletSystem61, EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;

    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 6;
    const float BulletSpacing = 10f;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedMultiplier = 0.1f;

    protected override IEnumerator Move()
    {
        float r = transform.eulerAngles.z;

        for (int i = 0; i < BranchCount; i++)
        {
            Vector3 pos = transform.position;

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (i * BranchSpacing) + (ii * BulletSpacing) + r;
                float s = BulletBaseSpeed + (ii * BulletSpeedMultiplier);

                bulletData.colour = bulletData.gradient.Evaluate(ii / (BulletCount - 1f));

                var bullet = SpawnBullet(2, z, pos, false);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }
        }

        yield return null;
        Destroy();
    }
}