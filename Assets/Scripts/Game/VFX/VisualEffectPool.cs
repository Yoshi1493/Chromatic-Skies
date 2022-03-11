using System.Collections.Generic;
using UnityEngine;

public class VisualEffectPool : MonoBehaviour
{
    public static VisualEffectPool Instance { get; private set; }

    readonly Queue<GameObject> vfxPool = new Queue<GameObject>();
    [SerializeField] GameObject visualEffect;

    void Awake()
    {
        Instance = this;
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
            GameObject newEffect = Instantiate(visualEffect, transform);
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