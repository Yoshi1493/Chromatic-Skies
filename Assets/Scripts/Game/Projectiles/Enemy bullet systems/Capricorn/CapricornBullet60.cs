using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBullet60 : ScriptableEnemyBullet<CapricornBulletSystem6, EnemyBullet>
{
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const float ShootingCooldown = 0.1f;

    protected override float MaxLifetime => Mathf.Infinity;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 0f, 1f);
        StartCoroutine(Shoot());

        transform.parent = ownerShip.transform;
        StartCoroutine(this.RotateAround(ownerShip, MaxLifetime, 90f));
    }

    IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = 0; i < BranchCount; i++)
            {
                float z = i * BranchSpacing;
                Vector3 pos = transform.position;

                SpawnBullet(1, z, pos, false).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}