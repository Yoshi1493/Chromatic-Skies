using System.Collections;
using static CoroutineHelper;

public class AriesBulletSystem3 : EnemyBulletSystem
{
    IEnumerator Start()
    {
        EnemyLaserPool.Instance.UpdatePoolableBullets(enemyLasers);
        yield return WaitForSeconds(3f);

        while (true)
        {
            SpawnLaser();
            yield return null;
            break;
        }
    }
}