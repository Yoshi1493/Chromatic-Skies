using System.Collections;
using static CoroutineHelper;

public class AriesBulletSystem3 : EnemyBulletSystem
{
    protected override IEnumerator Start()
    {
        yield return base.Start();

        while (true)
        {
            SpawnLaser();
            yield return null;
            break;
        }
    }
}