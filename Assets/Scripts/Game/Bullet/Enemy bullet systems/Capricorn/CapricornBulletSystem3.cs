using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem3 : EnemyBulletSystem
{
    protected override IEnumerator Start()
    {
		while (true)
		{
			yield return null;
		}        
    }
}