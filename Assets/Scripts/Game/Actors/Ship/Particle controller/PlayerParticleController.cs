using UnityEngine;
using UnityEngine.VFX;

public abstract class PlayerParticleController : ShipParticleController<Player>
{
    [SerializeField] protected VisualEffect specialVFX;
}