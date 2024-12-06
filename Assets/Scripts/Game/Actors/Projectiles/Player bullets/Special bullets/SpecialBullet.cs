using UnityEngine;

public abstract class SpecialBullet : PlayerBullet
{
    //[special bullet <-> enemy bullet] collision detection requires a collider in at least one party
    new protected Collider2D collider;

    protected override void Awake()
    {
        base.Awake();
        collider = GetComponent<Collider2D>();
    }
}