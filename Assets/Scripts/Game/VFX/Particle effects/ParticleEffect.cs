using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public abstract class ParticleEffect : MonoBehaviour
{
    public VisualEffect ParticleSystem { get; protected set; }
    protected IEnumerator particleAnimation;

    protected bool hasPlayed;

    protected virtual void Awake()
    {
        ParticleSystem = GetComponent<VisualEffect>();
    }

    protected virtual void OnEnable()
    {
        ParticleSystem.Play();
    }

    protected virtual void Update()
    {
        if (ParticleSystem.aliveParticleCount > 0)
        {
            hasPlayed = true;
        }

        if (ParticleSystem.aliveParticleCount == 0 && hasPlayed)
        {
            ReturnToPool();

            hasPlayed = false;
            return;
        }
    }

    protected abstract void ReturnToPool();
}