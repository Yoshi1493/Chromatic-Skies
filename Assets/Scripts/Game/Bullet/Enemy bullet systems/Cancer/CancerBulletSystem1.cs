using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem1 : EnemyBulletSystem
{
    IEnumerator Start()
    {
		while (true)
		{
			yield return null;
		}        
    }
}