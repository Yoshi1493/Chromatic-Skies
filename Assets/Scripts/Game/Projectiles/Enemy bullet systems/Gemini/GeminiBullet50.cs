using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBullet50 : ScriptableEnemyBullet<GeminiBulletSystem5, EnemyBullet>
{
    const float WaveSpacing = GeminiBulletSystem5.WaveSpacing;
    const int BranchCount = GeminiBulletSystem5.BranchCount;
    const float BranchSpacing = GeminiBulletSystem5.BranchSpacing;
    const float ShootingCooldown = 0.4f;

    protected override int NumCollisions => 0;
    protected override float MaxLifetime => Mathf.Infinity;

    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(2f);

        for (int i = 0; enabled; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BranchSpacing);
                Vector3 pos = transform.position;

                SpawnBullet(2, z, pos, false).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }

    protected override void Update()
    {
        base.Update();
        UpdatePosition();
    }

    void UpdatePosition()
    {
        Vector3 pos = ownerShip.transform.position;
        pos.x *= -1;
        transform.position = pos;
    }
}