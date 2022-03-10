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
            print("pool is empty; instantiating new effect...");
            GameObject newEffect = Instantiate(visualEffect, transform);
            newEffect.SetActive(false);

            return newEffect;
        }
    }

    public void ReturnToPool(GameObject returningEffect)
    {
        returningEffect.SetActive(false);
        vfxPool.Enqueue(returningEffect);
    }
}