using UnityEngine;
using UnityEngine.VFX;

public abstract class ShipParticleController<TShip> : MonoBehaviour
    where TShip : Ship
{
    protected TShip ship;

    [SerializeField] protected VisualEffect spawnVFX;
    [SerializeField] protected VisualEffect loseLifeVFX;
    [SerializeField] protected VisualEffect respawnVFX;
    [SerializeField] protected VisualEffect deathVFX;
    [SerializeField] protected VisualEffect invincibleVFX;

    protected virtual void Awake()
    {
        ship = GetComponentInParent<TShip>();

        ship.LoseLifeAction += OnShipLoseLife;
        ship.RespawnAction += OnShipRespawn;
        ship.DeathAction += OnShipDeath;
        ship.InvincibleAction += OnShipInvincible;

        if (loseLifeVFX.visualEffectAsset != null)
        {
            loseLifeVFX.SetVector4("ParticleColour", ship.shipData.UIColour.value);
        }

        if (deathVFX.visualEffectAsset != null)
        {
            deathVFX.SetVector4("ParticleColour", ship.shipData.UIColour.value);
        }

        if (invincibleVFX.visualEffectAsset != null)
        {
            invincibleVFX.SetVector4("ParticleColour", ship.shipData.UIColour.value);
        }
    }

    void OnShipLoseLife()
    {
        PlayVisualEffect(loseLifeVFX);
    }

    void OnShipRespawn()
    {
        PlayVisualEffect(respawnVFX);
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