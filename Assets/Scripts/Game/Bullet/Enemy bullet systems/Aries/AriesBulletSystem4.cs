using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem4 : EnemyBulletSystem
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (true)
		{
			yield return null;
		}        
    }
}