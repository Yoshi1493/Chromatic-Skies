using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] PlayerShooter shooter;

    void Awake()
    {
        player.TakeDamageAction += OnTakeDamage;
        player.LoseLifeAction += OnLoseLife;
        player.RespawnAction += OnRespawn;
        player.DeathAction += OnDie;
    }

    void Update()
    {
        if (!PauseHandler.IsPaused)
        {
            GetShootingInput();
        }
    }

    void GetShootingInput()
    {
        if (Input.GetButton("Shoot") && shooter.CanShoot)
        {
            AudioManager.Instance.PlaySound("player_shoot-default", true, 3);
        }
    }

    void OnTakeDamage(int _)
    {
        //play taken-damage sfx
    }

    void OnLoseLife()
    {
        //play lose-life sfx
    }

    void OnRespawn()
    {
        //play respawn sfx
    }

    void OnDie()
    {
        //play death sfx
    }
}