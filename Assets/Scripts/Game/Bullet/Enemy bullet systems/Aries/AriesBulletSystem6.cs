using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem6 : EnemyBulletSystem
{
    protected override IEnumerator Start()
	{
		yield return base.Start();

		while (true)
		{
			yield return null;
		}        
    }
}