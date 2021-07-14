using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem4 : EnemyBulletSystem
{
    protected override IEnumerator Start()
    {
		while (true)
		{
			yield return null;
		}        
    }
}