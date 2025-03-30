using UnityEngine;
using UnityEngine.VFX;

public class BossParticleController : ShipParticleController<Boss>
{
    [SerializeField] VisualEffect attackStartVFX;

    protected override void Awake()
    {
        base.Awake();

        parentShip.RespawnAction += OnShipRespawn;

        for (int i = 0; i < parentShip.bulletSystems.Count; i++)
        {
            parentShip.bulletSystems[i].StartAttackLoopAction += OnAttackLoopStart;
        }

        if (attackStartVFX.visualEffectAsset != null)
        {
            attackStartVFX.SetVector4("ParticleColour", parentShip.shipData.UIColour.value);
        }
    }

    void OnEnable()
    {
        PlayVisualEffect(spawnVFX);
    }

    void OnAttackLoopStart()
    {
        PlayVisualEffect(attackStartVFX);
    }

    void OnShipRespawn()
    {
        PlayVisualEffect(respawnVFX);
    }
}