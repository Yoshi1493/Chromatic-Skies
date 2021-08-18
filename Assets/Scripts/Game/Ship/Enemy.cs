public class Enemy : Ship
{
    protected override void LoseLife()
    {
        int currentSystem = shipData.Lives.OriginalValue - shipData.Lives.CurrentValue;

        base.LoseLife();

        if (shipData.Lives.CurrentValue > 0)
        {
            var currentEnemyShooters = transform.GetChild(currentSystem).GetComponents<EnemyShooter>();
            var nextEnemyShooters = transform.GetChild(currentSystem + 1).GetComponents<EnemyShooter>();

            for (int i = 0; i < currentEnemyShooters.Length; i++)
            {
                currentEnemyShooters[i].enabled = false;
            }

            for (int i = 0; i < nextEnemyShooters.Length; i++)
            {
                nextEnemyShooters[i].enabled = true;
            }
        }
    }

    protected override void Die()
    {
        gameObject.SetActive(false);
    }
}