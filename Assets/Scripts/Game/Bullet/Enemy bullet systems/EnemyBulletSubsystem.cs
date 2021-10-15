using UnityEngine;

public abstract class EnemyBulletSubsystem : EnemyShooter<EnemyBullet>
{
    protected override void OnLoseLife()
    {
        StopCoroutine(shootCoroutine);
    }
}