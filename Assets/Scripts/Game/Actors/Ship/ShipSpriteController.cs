using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class ShipSpriteController<TShip> : MonoBehaviour
    where TShip : Ship
{
    protected TShip ship;
    protected SpriteRenderer spriteRenderer;

    const float InvincibleAlpha = 0.5f;

    protected virtual void Awake()
    {
        ship = GetComponentInParent<TShip>();
        ship.InvincibleAction += OnSetInvincible;

        spriteRenderer = ship.SpriteRenderer;
        SetSpriteAlpha(0f);
    }

    void OnSetInvincible(bool state)
    {
        SetSpriteAlpha(state ? InvincibleAlpha : 1.0f);
    }

    protected void SetSpriteAlpha(float alpha)
    {
        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
        print(spriteRenderer.color.a);
    }
}