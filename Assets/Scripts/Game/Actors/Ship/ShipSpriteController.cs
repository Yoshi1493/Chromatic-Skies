using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class ShipSpriteController<TShip> : MonoBehaviour
    where TShip : Ship
{
    protected TShip parentShip;
    SpriteRenderer spriteRenderer;

    const float InvincibleAlpha = 0.5f;

    protected virtual void Awake()
    {
        parentShip = GetComponentInParent<TShip>();
        parentShip.InvincibleAction += OnSetInvincible;

        spriteRenderer = GetComponent<SpriteRenderer>();
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
    }
}