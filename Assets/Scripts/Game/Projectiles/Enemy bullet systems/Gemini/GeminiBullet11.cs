using System.Collections;
using static CoroutineHelper;

public class GeminiBullet11 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;

        if (endSpeed != GeminiBulletSystem11.BulletBaseSpeed)
        {
            yield return WaitForSeconds(1f);
            yield return this.LerpSpeed(GeminiBulletSystem11.BulletBaseSpeed, endSpeed, 1f);
        }
    }
}