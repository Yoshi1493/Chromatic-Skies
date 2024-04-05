using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestructionEffectPool : MonoBehaviour
{
    public static ProjectileDestructionEffectPool Instance { get; private set; }

    readonly Queue<GameObject> vfxPool = new();
    [SerializeField] GameObject vfx;

    new Transform transform;

    void Awake()
    {
        Instance = this;
        transform = GetComponent<Transform>();
    }

    public void DrainPool()
    {
        vfxPool.Clear();
    }

    public GameObject Get()
    {
        if (vfxPool.Count > 0)
        {
            return vfxPool.Dequeue();
        }
        else
        {
            GameObject newEffect = Instantiate(vfx, transform);
            Disable(newEffect);

            return newEffect;
        }
    }

    public void ReturnToPool(GameObject returningEffect)
    {
        Disable(returningEffect);
        vfxPool.Enqueue(returningEffect);
    }

    void Disable(GameObject go)
    {
        go.SetActive(false);
        go.GetComponent<ParticleEffect>().enabled = false;
    }
}