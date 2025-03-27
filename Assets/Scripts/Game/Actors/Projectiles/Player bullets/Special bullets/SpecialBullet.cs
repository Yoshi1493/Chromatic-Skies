using UnityEngine;

public abstract class SpecialBullet : PlayerBullet
{
    //[special bullet <-> boss bullet] collision detection requires a collider in at least one party
    new protected Collider2D collider;

    protected override void Awake()
    {
        base.Awake();
        collider = GetComponent<Collider2D>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        collider.enabled = true;
    }

    public override void Destroy()
    {
        base.Destroy();
        collider.enabled = false;
    }
}