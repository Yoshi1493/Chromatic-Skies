using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Actor : MonoBehaviour
{
    [HideInInspector] new public Transform transform;
    protected SpriteRenderer spriteRenderer;

    [HideInInspector] public Vector3 moveDirection;

    protected virtual void Awake()
    {
        transform = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Move(float moveSpeed)
    {
        Vector3 normalizedDirection = moveDirection.normalized;
        transform.Translate(Time.deltaTime * moveSpeed * normalizedDirection, Space.World);
    }
}