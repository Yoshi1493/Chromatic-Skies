using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem3 : EnemyBulletSystem
{
    protected override IEnumerator Start()
    {
		while (true)
		{
			yield return null;
		}        
    }
}