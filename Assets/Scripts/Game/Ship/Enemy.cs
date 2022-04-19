using System.Threading.Tasks;
using UnityEngine;

public class Enemy : Ship
{
    void Start()
    {
        FindObjectOfType<Player>().LoseLifeAction += OnPlayerLoseLife;
    }

    protected override void Update()
    {
        base.Update();

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.K))
            TakeDamage(shipData.CurrentHealth.Value);
#endif
    }

    protected override async void LoseLife()
    {
        int currentProjectileSystem = shipData.MaxLives.Value - shipData.CurrentLives.Value;

        base.LoseLife();

        if (shipData.CurrentLives.Value > 0)
        {
            var currentEnemyShooter = transform.GetChild(currentProjectileSystem).GetComponentInChildren<IEnemyAttack>();
            var nextEnemyShooter = transform.GetChild(currentProjectileSystem + 1).GetComponent<IEnemyAttack>();

            currentEnemyShooter.SetEnabled(false);

            StartCoroutine(this.ReturnToOriginalPosition());
            await Task.Delay(RespawnTime * 2);

            nextEnemyShooter.SetEnabled(true);
            SetSpriteAlpha(1f);
        }
    }

    protected override void Die()
    {
        gameObject.SetActive(false);
    }

    //disable and re-enable current projectile system upon player losing life
    async void OnPlayerLoseLife()
    {
        int currentProjectileSystem = shipData.MaxLives.Value - shipData.CurrentLives.Value;
        var currentEnemyShooters = transform.GetChild(currentProjectileSystem).GetComponentsInChildren<IEnemyAttack>();

        foreach (var enemyShooter in currentEnemyShooters)
        {
            enemyShooter.SetEnabled(false);
        }

        await Task.Delay(RespawnTime);

        //if player didn't kill enemy between delay
        if (currentProjectileSystem == shipData.MaxLives.Value - shipData.CurrentLives.Value)
        {
            currentEnemyShooters[0].SetEnabled(true);
        }
    }
}