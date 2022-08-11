using System.Collections;

public class CapricornBullet30 : EnemyBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        //float endSpeed = MoveSpeed * 0.5f;
        //yield return this.LerpSpeed(MoveSpeed, endSpeed, 1f);
        yield return null;
    }
}