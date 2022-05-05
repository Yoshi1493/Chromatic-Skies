using System.Collections;
using UnityEngine;

public class LeoBullet20 : ScriptableEnemyBullet<LeoBulletSystem2>
{
    const int BulletCount = 3;
    const int BulletSpacing = 360 / BulletCount;

    protected override float MaxLifetime => 3f;

    protected override IEnumerator Move()
    {
        float startSpeed = MoveSpeed;
        yield return this.LerpSpeed(startSpeed, 0f, 1f);
    }

    public override void Destroy()
    {
        float randOffset = Random.Range(0f, 60f);

        for (int i = 0; i < BulletCount; i++)
        {
            float z = (i * BulletSpacing) + randOffset;
            SpawnBullet(1, z, transform.position, false).Fire();
        }

        base.Destroy();
    }
}