using UnityEngine;

public abstract class Player : CharacterShip
{
    public override float RespawnTime => 3f;

    protected override float OriginalColliderRadius => 0.025f;
    protected override float InvincibleColliderRadius => 1f;

    [Space]

    [SerializeField] CircleCollider2D grazeCollider;
    [SerializeField] IntObject hitsTaken;

    protected PlayerSpecialShooter specialShooter;
    PauseHandler pauseHandler;

    protected override void Awake()
    {
        base.Awake();

        specialShooter = GetComponentInChildren<PlayerSpecialShooter>();
        pauseHandler = FindAnyObjectByType<PauseHandler>();
    }

    void Start()
    {
        TakeDamageAction += OnTakeDamage;
        InvincibleAction += OnInvincible;
        specialShooter.SpecialAction += OnSpecialActivated;
        pauseHandler.GamePauseAction += OnGamePaused;

        hitsTaken.value = 0;
    }

#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            TakeDamage(currentHealth.value);
    }
#endif

    void OnTakeDamage(int damage)
    {
        if (damage > 0)
        {
            hitsTaken.value++;
            SetInvincible(1f);
        }
    }

    void OnInvincible(bool state)
    {
        collider.enabled = !state;

        if (state)
        {
            DisplayInvincibleShield();
        }
    }

    protected abstract void OnSpecialActivated();

    void DisplayInvincibleShield()
    {
        var particleEffect = VFXObjectPool.Instance.Get((int)VFXType.InvinciblePlayerShield);

        particleEffect.transform.position = transform.position;
        particleEffect.gameObject.SetActive(true);

        particleEffect.ParticleSystem.SetFloat("ParticleSize", InvincibleColliderRadius);
        particleEffect.ParticleSystem.SetVector4("ParticleColour", shipData.UIColour.value);

        particleEffect.enabled = true;
    }

    void OnGamePaused(bool state)
    {
        collider.enabled = !state;
    }
}