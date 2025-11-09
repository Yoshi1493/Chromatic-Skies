using System.Collections;
using UnityEngine;

public abstract class Laser : Projectile
{
    Vector3 HitboxOffset => originalSize.y * 0.5f * transform.up;

    protected override int CollisionMask => 1 << LayerMask.NameToLayer("Player");
    protected override int NumCollisions => Physics2D.OverlapBoxNonAlloc(transform.position + HitboxOffset, activeSize, transform.eulerAngles.z, collisionResults, CollisionMask);
    protected bool IsColliding => NumCollisions > 0;

    protected Boss parentShip;

    protected bool active;
    protected Vector2 originalSize;
    protected Vector2 activeSize;
    const float WarningSpriteWidth = 0.04f;

    IEnumerator growAnimation;
    IEnumerator shrinkAnimation;

    [SerializeField] IntObject bossCurrentHealth;
    [SerializeField] AnimationCurve widthInterpolation;
    [SerializeField] AnimationCurve heightInterpolation;

    protected override void Awake()
    {
        base.Awake();

        originalSize = SpriteRenderer.size;
        activeSize = originalSize;

        parentShip = FindObjectOfType<Boss>();
        parentShip.LoseLifeAction += OnBossLoseLife;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        active = false;
    }

    public void Fire(float delay = 0.5f)
    {
        if (growAnimation != null)
        {
            StopCoroutine(growAnimation);
        }

        growAnimation = Grow(delay);
        StartCoroutine(growAnimation);
    }

    protected override void Update()
    {
        base.Update();

        if (active)
        {
            CheckCollisionWith<Player>();
            CheckCollisionWith<PlayerGraze>();
        }
    }

    protected override void HandleCollision(Collider2D coll)
    {
        //get particle spawn position+rotation
        Vector3 pos = coll.ClosestPoint(transform.position);
        float rot = coll.transform.position.GetRotationDifference(transform.position);

        if (coll.TryGetComponent(out Ship ship))
        {
            if (!ship.Invincible)
            {
                int damage = DamageCalculator.CalculateDamage(projectileData.Power.value, ship.shipData.Defense.Value, 10f);
                ship.TakeDamage(damage);
            }
        }
        else if (coll.TryGetComponent(out PlayerGraze playerGraze))
        {
            if (!hasGrazed)
            {
                playerGraze.GrazePlayer();
                SpawnGrazeParticles(pos, rot);

                hasGrazed = true;
            }
        }
    }

    IEnumerator Grow(float warningDuration, float lerpDuration = 0.1f)
    {
        if (warningDuration <= 0f)
        {
            if (warningDuration == 0f)
            {
                SpriteRenderer.size = originalSize;
                active = true;
            }

            growAnimation = null;
            yield break;
        }

        //continuously update SpriteRenderer initial size in case laser path changes length during warning delay (usually due to collisions)
        for (float _ = 0; _ < warningDuration; _ += Time.deltaTime)
        {
            SpriteRenderer.size = new(WarningSpriteWidth, IsColliding ? activeSize.y : originalSize.y);
            yield return null;
        }

        active = true;

        //determine animation start and end points after delay (in case it collides with something during warning delay)
        Vector2 currentSize = SpriteRenderer.size;
        Vector2 endSize = activeSize;
        float currentLerpTime = 0f;

        //animate laser from warning size to active size
        while (SpriteRenderer.size != endSize)
        {
            float lerpProgress = currentLerpTime / lerpDuration;

            float width = Mathf.Lerp(currentSize.x, endSize.x, widthInterpolation.Evaluate(lerpProgress / 2f));
            float height = Mathf.Lerp(currentSize.y, endSize.y, heightInterpolation.Evaluate(lerpProgress));

            SpriteRenderer.size = new Vector2(width, height);
            activeSize = SpriteRenderer.size;

            currentLerpTime += Time.deltaTime;
            yield return null;
        }

        growAnimation = null;
    }

    IEnumerator ShrinkAndDestroy(float lerpDuration = 0.1f)
    {
        float currentLerpTime = 0f;
        float startWidth = activeSize.x;

        while (currentLerpTime < lerpDuration)
        {
            float lerpProgress = currentLerpTime / lerpDuration;

            float width = Mathf.Lerp(startWidth, 0f, lerpProgress);
            float height = activeSize.y;
            SpriteRenderer.size = new Vector2(width, height);

            currentLerpTime += Time.deltaTime;
            yield return null;
        }

        active = false;
        BossLaserPool.Instance.ReturnToPool(this);
        shrinkAnimation = null;
    }

    void OnBossLoseLife()
    {
        Destroy(gameObject);
    }

    public override void Destroy()
    {
        if (growAnimation != null)
        {
            StopCoroutine(growAnimation);
        }

        if (shrinkAnimation == null)
        {
            shrinkAnimation = ShrinkAndDestroy();
            StartCoroutine(shrinkAnimation);
        }
    }

    void OnDisable()
    {
        active = false;
    }

    void OnDestroy()
    {
        parentShip.LoseLifeAction -= OnBossLoseLife;
    }

#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawCube(0.5f * SpriteRenderer.size.y * Vector3.up, SpriteRenderer.size);
        }
    }
#endif
}