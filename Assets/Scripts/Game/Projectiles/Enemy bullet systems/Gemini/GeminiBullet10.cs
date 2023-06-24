using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBullet10 : ScriptableEnemyBullet<GeminiBulletSystem1, EnemyBullet>
{
    const int WaveCount = 20;
    const int BranchCount = 1;
    const float BranchSpacing = 360f / BranchCount;
    const float ShootingCooldown = 0.1f;

    protected override float MaxLifetime => 4f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(2f, 0f, 1f);
        this.LookAt(ownerShip);

        StartCoroutine(Shoot());
        yield return this.RotateAround(ownerShip, 2f, 180f);

        MoveSpeed = 0f;
        yield return WaitForSeconds(1f);
    }

    IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            float r = transform.eulerAngles.z;

            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (ii * BranchSpacing) + r;
                Vector3 pos = transform.position;

                SpawnBullet(1, z, pos, false).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}