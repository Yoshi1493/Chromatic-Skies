using UnityEngine;
using UnityEngine.VFX;

public abstract class PlayerParticleController : ShipParticleController<Player>
{
    [SerializeField] protected VisualEffect specialVFX;

    protected override void Awake()
    {
        base.Awake();
        parentShip.RespawnAction += OnShipRespawn;
    }

    void OnShipRespawn()
    {
        PlayVisualEffect(respawnVFX);
    }
}