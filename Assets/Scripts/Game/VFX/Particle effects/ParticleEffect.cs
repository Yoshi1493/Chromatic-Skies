using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class ParticleEffect : MonoBehaviour
{
    public VisualEffect ParticleSystem { get; protected set; }
    protected IEnumerator particleAnimation;

    protected virtual void Awake()
    {
        ParticleSystem = GetComponent<VisualEffect>();
    }

    void OnEnable()
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
        ParticleSystem.Play();
        yield return null;
    }
}