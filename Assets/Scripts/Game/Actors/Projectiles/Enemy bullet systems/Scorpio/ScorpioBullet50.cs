using System.Collections;
using UnityEngine;

public class ScorpioBullet50 : ScriptableEnemyBullet<ScorpioBulletSystem5, EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;

    const int WaveCount = 8;
    const int BulletCount = 6;
    const float BulletSpacing = 360f / BulletCount;
    const float ShootingCooldown = 0.8f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 3f;
        yield return null;
    }
}