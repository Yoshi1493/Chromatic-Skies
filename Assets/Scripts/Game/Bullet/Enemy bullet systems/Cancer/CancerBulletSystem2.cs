using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem2 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
		while (true)
		{
			yield return null;
		}        
    }
}