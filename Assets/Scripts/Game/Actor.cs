using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    [HideInInspector] new public Transform transform;
    protected SpriteRenderer spriteRenderer;

    protected Vector2 moveDirection;

    protected virtual void Awake()
    {
        transform = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Move(float moveSpeed, Space space = Space.Self)
    {
        Vector3 normalizedDirection = moveDirection.normalized;
        transform.Translate(Time.deltaTime * moveSpeed * normalizedDirection, space);
    }
}