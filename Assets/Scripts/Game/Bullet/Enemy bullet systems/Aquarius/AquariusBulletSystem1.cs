using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem1 : EnemyBulletSystem
{
    protected override IEnumerator Start()
    {
		while (true)
		{
			yield return null;
		}        
    }
}