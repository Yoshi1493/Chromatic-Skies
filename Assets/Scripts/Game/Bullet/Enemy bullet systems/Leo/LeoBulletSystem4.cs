using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem4 : EnemyBulletSystem
{
    protected override IEnumerator Start()
    {
		while (true)
		{
			yield return null;
		}        
    }
}