using System.Threading.Tasks;
using UnityEngine;

public class Player : Ship
{
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.L))
            TakeDamage(currentHealth);
#endif
    }

    //to-do: improve scalability?
    protected override async void LoseLife()
    {
        base.LoseLife();

        if (currentLives > 0)
        {
            await Task.Delay(RespawnTime);
            Respawn();

            invincible = false;
            collider.enabled = true;
        }
    }

    protected override void Die()
    {
        spriteRenderer.enabled = false;
        enabled = false;
    }
}