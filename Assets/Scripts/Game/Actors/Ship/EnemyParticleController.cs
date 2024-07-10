using UnityEngine;
using UnityEngine.VFX;

public class EnemyParticleController : ShipParticleController<Enemy>
{
    [SerializeField] VisualEffect attackStartVFX;

    protected override void Awake()
    {
        base.Awake();
        ship.StartAttackAction += OnAttackStart;
    }

    void OnAttackStart(int _)
    {
        PlayVisualEffect(attackStartVFX);
    }
}