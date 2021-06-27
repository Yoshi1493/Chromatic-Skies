using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem2 : EnemyBulletSystem
{
    IEnumerator Start()
    {
		while (true)
		{
			yield return null;
		}        
    }
}