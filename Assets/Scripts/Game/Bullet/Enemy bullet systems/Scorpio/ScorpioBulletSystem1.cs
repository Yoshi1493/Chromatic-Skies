using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem1 : EnemyBulletSystem
{
    IEnumerator Start()
    {
		while (true)
		{
			yield return null;
		}        
    }
}