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

    protected void Move(float moveSpeed)
    {
        Vector3 normalizedDirection = moveDirection.normalized;

        transform.Translate(Time.deltaTime * moveSpeed * normalizedDirection, Space.World);
        transform.eulerAngles = Mathf.Atan2(-normalizedDirection.x, normalizedDirection.y) * Mathf.Rad2Deg * Vector3.forward;
    }
}