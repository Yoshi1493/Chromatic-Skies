using UnityEngine;
using UnityEngine.VFX;

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
        ship.RespawnAction += OnShipRespawn;
        ship.DeathAction += OnShipDeath;
        ship.InvincibleAction += OnShipInvincible;

        loseLifeVFX.SetVector4("ParticleColour", ship.shipData.UIColour.value);
        deathVFX.SetVector4("ParticleColour", ship.shipData.UIColour.value);
    }

    void OnShipLoseLife()
    {
        loseLifeVFX.SendEvent("OnPlay");
    }

    void OnShipRespawn()
    {
        respawnVFX.SendEvent("OnPlay");
    }

    void OnShipDeath()
    {
        deathVFX.SendEvent("OnPlay");
    }

    void OnShipInvincible(bool state)
    {
        invincibleVFX.SendEvent("OnPlay");
    }
}