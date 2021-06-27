using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem3 : EnemyBulletSystem
{
    IEnumerator Start()
    {
		while (true)
		{
			yield return null;
		}        
    }
}