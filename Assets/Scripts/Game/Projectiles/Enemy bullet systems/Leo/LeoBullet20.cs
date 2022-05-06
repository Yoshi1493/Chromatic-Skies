using System.Collections;

public class LeoBullet20 : ScriptableEnemyBullet<LeoBulletSystem2>
{
    const int BulletCount = 6;
    const int BulletSpacing = 360 / BulletCount;

    protected override float MaxLifetime => 2.5f;

    protected override IEnumerator Move()
    {
        float startSpeed = MoveSpeed;
        yield return this.LerpSpeed(startSpeed, 0f, 1f);
    }

    public override void Destroy()
    {
        float theta = transform.eulerAngles.z;

        for (int i = 0; i < BulletCount; i++)
        {
            float z = theta + (i * BulletSpacing);
            SpawnBullet(1, z, transform.position, false).Fire();
        }

        base.Destroy();
    }
}