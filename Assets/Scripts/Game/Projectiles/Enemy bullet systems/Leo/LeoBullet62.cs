using System.Collections;

public class LeoBullet62 : ScriptableEnemyBullet<LeoBulletSystem62, EnemyBullet>
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 2f, 2f);
    }
}