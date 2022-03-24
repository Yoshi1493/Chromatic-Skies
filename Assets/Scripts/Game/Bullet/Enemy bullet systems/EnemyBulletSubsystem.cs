using UnityEngine;

public abstract class EnemyBulletSubsystem<TProjectile> : EnemyShooter<TProjectile>
    where TProjectile : Projectile
{
    protected float camHalfHeight;
    protected float camHalfWidth;

    protected override void Awake()
    {
        base.Awake();

        Camera mainCam = Camera.main;

        camHalfHeight = mainCam.orthographicSize;
        camHalfWidth = camHalfHeight * mainCam.aspect;
    }
}