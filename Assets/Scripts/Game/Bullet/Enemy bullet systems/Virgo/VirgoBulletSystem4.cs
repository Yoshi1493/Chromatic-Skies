using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem4 : EnemyBulletSystem
{
    IEnumerator Start()
    {
		while (true)
		{
			yield return null;
		}        
    }
}