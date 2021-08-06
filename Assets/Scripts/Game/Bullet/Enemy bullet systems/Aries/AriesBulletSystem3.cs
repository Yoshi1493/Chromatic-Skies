using System.Collections;
using static CoroutineHelper;

public class AriesBulletSystem3 : EnemyBulletSystem
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (true)
        {
            yield return null;
            break;
        }
    }
}