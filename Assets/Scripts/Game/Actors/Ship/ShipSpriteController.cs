using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class ShipSpriteController<TShip> : MonoBehaviour
    where TShip : Ship
{
    protected TShip parentShip;
    SpriteRenderer spriteRenderer;

    const float InvincibleAlpha = 0.25f;

    protected virtual void Awake()
    {
        parentShip = GetComponentInParent<TShip>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        parentShip.InvincibleAction += OnSetInvincible;
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