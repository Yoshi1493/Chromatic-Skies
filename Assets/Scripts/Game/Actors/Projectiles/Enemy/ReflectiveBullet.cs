using UnityEngine;

[RequireComponent(typeof(Bullet))]
public class ReflectiveBullet : MonoBehaviour
{
    [SerializeField] ProjectileObject projectileData;
    [SerializeField] Bullet bulletComponent;

    [SerializeField] int maxReflectCount = 1;
    int currentReflectCount;

    const float ReflectCollisionThreshold = 0.01f;

    void OnEnable()
    {
        currentReflectCount = maxReflectCount;
    }

    public void HandleReflection(Collider2D coll)
    {
        currentReflectCount--;
        if (currentReflectCount < 0) return;

        Vector3 point = coll.ClosestPoint(bulletComponent.transform.position);
        Vector3 dir = bulletComponent.moveDirection;

        if (Mathf.Abs(point.x - bulletComponent.transform.position.x) < ReflectCollisionThreshold)
        {
            dir.y *= -1;
        }
        if (Mathf.Abs(point.y - bulletComponent.transform.position.y) < ReflectCollisionThreshold)
        {
            SpawnReflectionParticles(point);
            dir.x *= -1;
        }

        bulletComponent.moveDirection = dir;
        bulletComponent.SpriteRenderer.color = projectileData.gradient.Evaluate(Mathf.Max(currentReflectCount, 0f) / maxReflectCount);
    }

    void SpawnReflectionParticles(Vector3 spawnPos)
    {
        var particleEffect = VFXObjectPool.Instance.Get((int)VFXType.BulletReflection);

        particleEffect.transform.position = spawnPos;
        particleEffect.gameObject.SetActive(true);

        particleEffect.ParticleSystem.SetVector4("ParticleColour", bulletComponent.SpriteRenderer.color);
        particleEffect.ParticleSystem.SetFloat("ParticleRotation", Mathf.Sign(bulletComponent.moveDirection.x) * 90f);

        particleEffect.enabled = true;
    }
}
