using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class ShipSpriteController<TShip> : MonoBehaviour
    where TShip : Ship
{
    protected TShip parentShip;
    SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        parentShip = GetComponentInParent<TShip>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        parentShip.LoseLifeAction += OnShipLoseLife;
        parentShip.RespawnAction += OnShipRespawn;
    }

    void OnShipLoseLife()
    {
        SetSpriteAlpha(0.25f);
    }

    void OnShipRespawn()
    {
        SetSpriteAlpha(1.0f);
    }

    void SetSpriteAlpha(float alpha)
    {
        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
    }
}