using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem5 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
		while (true)
		{
			yield return null;
		}        
    }
}