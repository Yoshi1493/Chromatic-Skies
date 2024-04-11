using UnityEngine;

public class Player : Ship
{
    public override float RespawnTime => 2f;

    protected override void Awake()
    {
        base.Awake();
        TakeDamageAction += OnTakeDamage;
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
}