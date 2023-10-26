using System.Collections;
using UnityEngine;

public class AquariusBullet60 : ScriptableEnemyBullet<AquariusBulletSystem6, EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;

    const int BulletCount = 24;
    const float BulletSpacing = 360f / BulletCount;

    protected override float MaxLifetime => 1f;

    protected override IEnumerator Move()
    {
        float startSpeed = MoveSpeed;
        yield return this.LerpSpeed(startSpeed, 0f, 1f);
    }

    public override void Destroy()
    {
        for (int i = 0; i < BulletCount; i++)
        {
            float z = (i * BulletSpacing);
            Vector3 pos = transform.position;

            SpawnBullet(1, z, pos, false).Fire();
        }

        base.Destroy();
    }
}