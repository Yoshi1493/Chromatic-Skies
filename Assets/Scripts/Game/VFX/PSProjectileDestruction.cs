using System.Collections;
using UnityEngine;
using UnityEngine.VFX;
using static CoroutineHelper;

public class PSProjectileDestruction : MonoBehaviour
{
    public VisualEffect ParticleSystem;

    void Awake()
    {
        ParticleSystem = GetComponent<VisualEffect>();
    }

    IEnumerator Start()
    {
        yield return WaitUntil(() => !ParticleSystem.culled);
        print("particles fully invisible; returning to pool.");
        VisualEffectPool.Instance.ReturnToPool(this.gameObject);
    }
}