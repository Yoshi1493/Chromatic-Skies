using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBullet60 : ScriptableEnemyBullet<SagittariusBulletSystem6, EnemyBullet>
{
    const int BranchCount = 12;
    const float BranchSpacing = 360f / BranchCount;
    const float ShootingCooldown = 0.1f;

    protected override float MaxLifetime => Mathf.Infinity;

    protected override IEnumerator Move()
    {
        transform.parent = ownerShip.transform;
        yield return this.LerpSpeed(3f, 0f, 1f);

        yield return WaitForSeconds(1f);

        StartCoroutine(this.TransformRotateAround(ownerShip.transform.position, MaxLifetime, 60f));

        int i = 0;

        for (int ii = 0; enabled; ii++)
        {
            for (int iii = 0; iii < BranchCount; iii++)
            {
                float z = i + (iii * BranchSpacing);
                Vector3 pos = transform.position;

                var bullet = SpawnBullet(1, z, pos, false) as SagittariusBullet61;
                if (bullet.parentBullet != this)
                {
                    bullet.parentBullet = this;
                }
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
            i += ii;
        }
    }
}