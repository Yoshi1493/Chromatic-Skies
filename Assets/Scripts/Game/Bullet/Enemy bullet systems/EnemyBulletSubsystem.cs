using UnityEngine;

public abstract class EnemyBulletSubsystem : EnemyBulletSystem
{
    protected override void OnLoseLife()
    {
        StopCoroutine(shootCoroutine);
    }
}