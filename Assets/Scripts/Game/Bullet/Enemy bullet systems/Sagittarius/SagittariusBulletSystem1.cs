using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem1 : EnemyBulletSystem
{
    IEnumerator Start()
    {
		while (true)
		{
			yield return null;
		}        
    }
}