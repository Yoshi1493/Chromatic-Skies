using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    [HideInInspector] new public Transform transform;
    public SpriteRenderer SpriteRenderer { get; protected set; }

    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public float MoveSpeed { get; set; }

    protected virtual void Awake()
    {
        transform = GetComponent<Transform>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
}