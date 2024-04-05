using System.Collections;
using UnityEngine;
using UnityEngine.VFX;
using static CoroutineHelper;

public class ParticleEffect : MonoBehaviour
{
    public VisualEffect ParticleSystem { get; protected set; }
    protected IEnumerator particleAnimation;

    protected int OnPlayEventID = Shader.PropertyToID("OnPlay");

    void Awake()
    {
        ParticleSystem = GetComponent<VisualEffect>();
    }

    protected void OnEnable()
    {
        PlayAnimation();
    }

    protected void PlayAnimation()
    {
        if (particleAnimation != null)
            StopCoroutine(particleAnimation);

        particleAnimation = Play();
        StartCoroutine(particleAnimation);
    }

    protected virtual IEnumerator Play()
    {
        ParticleSystem.SendEvent(OnPlayEventID);

        yield return WaitUntil(() => ParticleSystem.aliveParticleCount == 0);
        VisualEffectPool.Instance.ReturnToPool(gameObject);
    }
}