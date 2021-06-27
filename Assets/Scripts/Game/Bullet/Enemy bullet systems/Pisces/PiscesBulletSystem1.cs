using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem1 : EnemyBulletSystem
{
    IEnumerator Start()
    {
		while (true)
		{
			yield return null;
		}        
    }
}