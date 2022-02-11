using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    [HideInInspector] new public Transform transform;
    protected SpriteRenderer spriteRenderer;

    [HideInInspector] public Vector3 moveDirection;

    protected virtual void Awake()
    {
        transform = GetComponent<Transform>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Move(Vector3 direction, float speed)
    {
        transform.Translate(Time.deltaTime * speed * direction, Space.World);
    }
}