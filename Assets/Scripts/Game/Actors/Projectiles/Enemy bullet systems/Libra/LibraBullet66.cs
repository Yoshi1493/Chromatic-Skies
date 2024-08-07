using System.Collections;
using static CoroutineHelper;

public class LibraBullet66 : EnemyBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(2f);
        MoveSpeed = 2f;
    }
}