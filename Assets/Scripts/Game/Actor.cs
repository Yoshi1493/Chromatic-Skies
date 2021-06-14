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

    protected virtual void Move(float moveSpeed)
    {
        Vector3 normalizedDirection = moveDirection.normalized;
        transform.position += Time.deltaTime * moveSpeed * normalizedDirection;
    }
}