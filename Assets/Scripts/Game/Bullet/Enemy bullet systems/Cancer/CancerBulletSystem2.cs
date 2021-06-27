using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem2 : EnemyBulletSystem
{
    IEnumerator Start()
    {
		while (true)
		{
			yield return null;
		}        
    }
}