using System.Collections.Generic;
using UnityEngine;

public class VFXObjectPool : MonoBehaviour
{
    public static VFXObjectPool Instance { get; private set; }

    readonly Dictionary<VFXType, Queue<GameObject>> vfxPool = new();
    [SerializeField] List<GameObject> visualEffects;

    new Transform transform;

    void Awake()
    {
        Instance = this;
        transform = GetComponent<Transform>();
    }

    void Start()
    {
        for (int i = 0; i < visualEffects.Count; i++)
        {
            if (System.Enum.IsDefined(typeof(VFXType), i))
            {
                vfxPool.Add((VFXType)i, new Queue<GameObject>());
            }
        }
    }

    public GameObject Get(VFXType vfxType)
    {
        if (vfxPool[vfxType].Count > 0)
        {
            return vfxPool[vfxType].Dequeue();
        }
        else
        {
            GameObject newEffect = Instantiate(visualEffects[(int)vfxType], transform);
            Disable(newEffect);

            return newEffect;
        }
    }

    public void ReturnToPool(GameObject returningEffect, VFXType vfxType)
    {
        Disable(returningEffect);
        vfxPool[vfxType].Enqueue(returningEffect);
    }

    void Disable(GameObject go)
    {
        go.SetActive(false);
        go.GetComponent<ParticleEffect>().enabled = false;
    }
}

public enum VFXType
{
    ProjectileDestruction = 0,
    InvincibleShield = 1
}