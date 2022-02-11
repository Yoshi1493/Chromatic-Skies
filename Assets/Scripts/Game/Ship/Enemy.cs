using System.Threading.Tasks;
using UnityEngine;

public class Enemy : Ship
{
    protected override async void LoseLife()
    {
        int currentProjectileSystem = shipData.MaxLives.Value - shipData.CurrentLives.Value;

        base.LoseLife();

        await Task.Yield();

        if (shipData.CurrentLives.Value > 0)
        {
            var currentEnemyShooters = transform.GetChild(currentProjectileSystem).GetComponentsInChildren<IEnemyAttack>();
            var nextEnemyShooters = transform.GetChild(currentProjectileSystem + 1).GetComponents<IEnemyAttack>();

            for (int i = 0; i < currentEnemyShooters.Length; i++)
            {
                currentEnemyShooters[i].SetEnabled(false);
            }

            for (int i = 0; i < nextEnemyShooters.Length; i++)
            {
                nextEnemyShooters[i].SetEnabled(true);
            }
        }
    }

    #region DEBUG
#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            TakeDamage(shipData.CurrentHealth.Value);
    }
#endif
    #endregion

    protected override void Die()
    {
        gameObject.SetActive(false);
    }
}