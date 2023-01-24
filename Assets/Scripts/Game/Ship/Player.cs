using System.Threading.Tasks;
using UnityEngine;

public class Player : Ship
{
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

    //to-do: improve scalability?
    protected override async void LoseLife()
    {
        base.LoseLife();

        if (currentLives > 0)
        {
            await Task.Delay(RespawnTime);

            Respawn();
            collider.enabled = true;
        }
    }

    protected override void Die()
    {
        spriteRenderer.enabled = false;
        enabled = false;
    }
}