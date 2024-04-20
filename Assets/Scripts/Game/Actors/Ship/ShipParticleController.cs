using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Ship))]
public class ShipParticleController : MonoBehaviour
{
    Ship ship;

    [SerializeField] VisualEffect loseLifeVFX;
    [SerializeField] VisualEffect respawnVFX;
    [SerializeField] VisualEffect deathVFX;
    [SerializeField] VisualEffect invincibleVFX;

    void Awake()
    {
        ship = GetComponentInParent<Ship>();

        ship.LoseLifeAction += OnShipLoseLife;
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

    void OnShipLoseLife()
    {
        if (loseLifeVFX.visualEffectAsset != null)
        {
            loseLifeVFX.SendEvent("OnPlay");
        }

        if (ship.currentLives > 0)
        {
            if (respawnVFX.visualEffectAsset != null)
            {
                respawnVFX.SendEvent("OnPlay");
            }
        }
    }

    void OnShipDeath()
    {
        if (deathVFX.visualEffectAsset != null)
        {
            deathVFX.SendEvent("OnPlay");
        }
    }

    void OnShipInvincible(bool state)
    {
        if (invincibleVFX.visualEffectAsset != null)
        {
            invincibleVFX.SendEvent("OnPlay");
        }
    }
}