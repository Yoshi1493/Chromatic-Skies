using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem4 : EnemyBulletSystem
{
    protected override IEnumerator Shoot()
    {
		while (true)
		{
			yield return null;
		}        
    }
}