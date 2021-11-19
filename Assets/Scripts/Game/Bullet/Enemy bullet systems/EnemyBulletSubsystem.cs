public abstract class EnemyBulletSubsystem<TProjectile> : EnemyShooter<TProjectile>
    where TProjectile : Projectile
{
    protected override void OnLoseLife()
    {
        StopCoroutine(shootCoroutine);
    }
}