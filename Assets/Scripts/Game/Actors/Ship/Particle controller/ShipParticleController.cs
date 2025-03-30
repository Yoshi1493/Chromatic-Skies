using UnityEngine;
using UnityEngine.VFX;

public abstract class ShipParticleController<TShip> : MonoBehaviour
    where TShip : Ship
{
    protected TShip parentShip;

    [SerializeField] protected VisualEffect spawnVFX;
    [SerializeField] protected VisualEffect loseLifeVFX;
    [SerializeField] protected VisualEffect respawnVFX;
    [SerializeField] protected VisualEffect deathVFX;
    [SerializeField] protected VisualEffect invincibleVFX;

    protected virtual void Awake()
    {
        parentShip = GetComponentInParent<TShip>();

        parentShip.LoseLifeAction += OnShipLoseLife;
        parentShip.DeathAction += OnShipDeath;
        parentShip.InvincibleAction += OnShipInvincible;

        if (loseLifeVFX.visualEffectAsset != null)
        {
            loseLifeVFX.SetVector4("ParticleColour", parentShip.shipData.UIColour.value);
        }

        if (respawnVFX.visualEffectAsset != null)
        {
            respawnVFX.SetVector4("ParticleColour", parentShip.shipData.UIColour.value);
        }

        if (deathVFX.visualEffectAsset != null)
        {
            deathVFX.SetVector4("ParticleColour", parentShip.shipData.UIColour.value);
        }

        if (invincibleVFX.visualEffectAsset != null)
        {
            invincibleVFX.SetVector4("ParticleColour", parentShip.shipData.UIColour.value);
        }
    }

    void OnShipLoseLife()
    {
        PlayVisualEffect(loseLifeVFX);
    }

    void OnShipDeath()
    {
        PlayVisualEffect(deathVFX);
    }

    void OnShipInvincible(bool state)
    {
        //PlayVisualEffect(invincibleVFX);
    }

    protected void PlayVisualEffect(VisualEffect vfx)
    {
        if (vfx.visualEffectAsset != null)
        {
            vfx.Reinit();
            vfx.Play();
        }
    }

    protected void StopVisualEffect(VisualEffect vfx)
    {
        if (vfx.visualEffectAsset != null)
        {
            vfx.Stop();
        }
    }
}