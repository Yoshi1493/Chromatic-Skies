using System.Collections;
using static MathHelper;

public class LeoBullet20 : ScriptableEnemyBullet<LeoBulletSystem2, EnemyBullet>
{
    const int BulletCount = 5;
    const int BulletSpacing = 360 / BulletCount;

    protected override float MaxLifetime => 2f;

    protected override IEnumerator Move()
    {
        float startSpeed = MoveSpeed;
        yield return this.LerpSpeed(startSpeed, 0f, 1f);
    }

    public override void Destroy()
    {
        float r = RandomAngleDeg;

        for (int i = 0; i < BulletCount; i++)
        {
            float z = (i * BulletSpacing) + r;
            SpawnBullet(1, z, transform.position, false).Fire();
        }

        base.Destroy();
    }
}