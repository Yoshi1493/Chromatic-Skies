using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    [HideInInspector] new public Transform transform;
    public SpriteRenderer SpriteRenderer { get; protected set; }

    protected virtual void Awake()
    {
        transform = GetComponent<Transform>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
}