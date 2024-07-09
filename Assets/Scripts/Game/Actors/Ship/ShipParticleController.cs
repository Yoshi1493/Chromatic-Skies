using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Ship))]
public class ShipParticleController : MonoBehaviour
{
    Ship ship;

    [SerializeField] VisualEffect spawnVFX;
    [SerializeField] VisualEffect loseLifeVFX;
    [SerializeField] VisualEffect respawnVFX;
    [SerializeField] VisualEffect deathVFX;
    [SerializeField] VisualEffect invincibleVFX;

    void Awake()
    {
        ship = GetComponentInParent<Ship>();

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
    }

    void Start()
    {
        PlayVisualEffect(spawnVFX);
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
        PlayVisualEffect(invincibleVFX);
    }

    void PlayVisualEffect(VisualEffect vfx)
    {
        if (vfx.visualEffectAsset != null)
        {
            vfx.Reinit();
            vfx.SendEvent("OnPlay");
        }
    }
}