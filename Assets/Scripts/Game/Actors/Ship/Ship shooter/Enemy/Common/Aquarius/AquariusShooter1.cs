using System.Collections;
using UnityEngine;

public class AquariusShooter1 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        yield return null;
    }
}