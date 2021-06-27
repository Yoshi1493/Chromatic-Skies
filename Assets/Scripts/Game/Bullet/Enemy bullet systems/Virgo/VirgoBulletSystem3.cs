using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem3 : EnemyBulletSystem
{
    IEnumerator Start()
    {
		while (true)
		{
			yield return null;
		}        
    }
}