using UnityEngine;

public class Player : Ship
{
    public override float RespawnTime => 3f;

    protected override float OriginalColliderRadius => 0.025f;
    protected override float InvincibleColliderRadius => 1f;

    PauseHandler pauseHandler;

    protected override void Awake()
    {
        base.Awake();
        pauseHandler = FindObjectOfType<PauseHandler>();
    }

    void Start()
    {
        TakeDamageAction += OnTakeDamage;
        InvincibleAction += OnInvincible;

        pauseHandler.GamePauseAction += OnGamePaused;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.L))
            TakeDamage(currentHealth);
#endif
    }

    void OnTakeDamage()
    {
        SetInvincible(1f);
    }

    void OnInvincible(bool state)
    {
        collider.enabled = !state;

        if (state)
        {
            DisplayInvincibleShield(transform.position);
        }
    }

    void OnGamePaused(bool state)
    {
        collider.enabled = !state;
    }

    public override void DisplayInvincibleShield(Vector3 _)
    {
        GameObject vfx = VFXObjectPool.Instance.Get(VFXType.InvinciblePlayerShield);
        var particleEffect = vfx.GetComponent<ParticleEffect>();

        particleEffect.transform.position = transform.position;
        vfx.SetActive(true);

        particleEffect.ParticleSystem.SetFloat("ParticleSize", InvincibleColliderRadius);
        particleEffect.ParticleSystem.SetVector4("ParticleColour", shipData.UIColour.value);

        particleEffect.enabled = true;
    }
}