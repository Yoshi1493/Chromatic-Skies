using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public abstract class ParticleEffect : MonoBehaviour
{
    public VisualEffect ParticleSystem { get; protected set; }
    protected IEnumerator particleAnimation;

    protected int OnPlayEventID = Shader.PropertyToID("OnPlay");

    void Awake()
    {
        ParticleSystem = GetComponent<VisualEffect>();
    }

    protected void PlayAnimation()
    {
        if (particleAnimation != null)
            StopCoroutine(particleAnimation);

        particleAnimation = Play();
        StartCoroutine(particleAnimation);
    }

    protected abstract IEnumerator Play();
}