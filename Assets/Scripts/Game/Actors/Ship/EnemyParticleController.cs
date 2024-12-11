using UnityEngine;
using UnityEngine.VFX;

public class EnemyParticleController : ShipParticleController<Enemy>
{
    [SerializeField] VisualEffect attackStartVFX;

    protected override void Awake()
    {
        base.Awake();

        parentShip.StartAttackAction += OnAttackStart;

        for (int i = 0; i < parentShip.bulletSystems.Count; i++)
        {
            parentShip.bulletSystems[i].StartAttackLoopAction += OnAttackLoopStart;
        }
    }

    void OnAttackStart(int _)
    {
        PlayVisualEffect(spawnVFX);
    }

    void OnAttackLoopStart()
    {
        PlayVisualEffect(attackStartVFX);
    }
}