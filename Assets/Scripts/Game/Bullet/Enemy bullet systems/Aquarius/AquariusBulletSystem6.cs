using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem6 : EnemyBulletSystem
{
    protected override IEnumerator Start()
    {
		while (true)
		{
			yield return null;
		}        
    }
}