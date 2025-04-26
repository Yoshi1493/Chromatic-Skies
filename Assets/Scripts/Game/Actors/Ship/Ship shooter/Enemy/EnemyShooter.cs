using UnityEngine;

public abstract class EnemyShooter<TProjectile> : Shooter<TProjectile>
    where TProjectile : Projectile
{
    protected Player playerShip;
    protected Vector3 PlayerPosition => playerShip.transform.position;

    [Space]
    [SerializeField] protected ProjectileObject bulletData;

    protected override void Awake()
    {
        base.Awake();
        playerShip = FindObjectOfType<Player>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine);
        }

        shootCoroutine = Shoot();
        StartCoroutine(shootCoroutine);
    }

    protected override void OnLoseLife()
    {
        StopAllCoroutines();
    }
}