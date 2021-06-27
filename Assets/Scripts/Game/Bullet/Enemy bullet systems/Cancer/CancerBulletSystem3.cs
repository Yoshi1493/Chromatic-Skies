using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem3 : EnemyBulletSystem
{
    IEnumerator Start()
    {
		while (true)
		{
			yield return null;
		}        
    }
}