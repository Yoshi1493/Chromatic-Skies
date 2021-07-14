using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem3 : EnemyBulletSystem
{
    protected override IEnumerator Start()
    {
		while (true)
		{
			yield return null;
		}        
    }
}