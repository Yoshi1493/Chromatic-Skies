using System.Collections;
using static CoroutineHelper;

public class CancerBullet40 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 8f;

        while (enabled)
        {
            yield return WaitForSeconds(0.25f);
            yield return this.RotateBy(180f, 0.1f);
            MoveSpeed = 8f;
            yield return WaitForSeconds(0.25f);
        }
    }
}