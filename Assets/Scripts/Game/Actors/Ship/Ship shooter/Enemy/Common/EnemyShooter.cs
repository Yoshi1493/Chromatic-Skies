using System.Collections;
using UnityEngine;

public abstract class EnemyShooter<TProjectile> : Shooter<TProjectile>
    where TProjectile : Projectile
{
    protected Player playerShip;
    protected Vector3 PlayerPosition => playerShip.transform.position;

    protected float screenHalfHeight;
    protected float screenHalfWidth;

    [Space]

    [SerializeField] protected ProjectileObject bulletData;

    protected override void Awake()
    {
        base.Awake();

        //find player
        playerShip = FindObjectOfType<Player>();

        //determine screen dimensions
        Camera mainCam = Camera.main;
        screenHalfHeight = mainCam.orthographicSize;
        screenHalfWidth = screenHalfHeight * mainCam.aspect;
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
        base.OnLoseLife();
    }
}