using UnityEngine;

public class ShipParticleController : MonoBehaviour
{
    Player player;
    Enemy enemy;

    [SerializeField] GameObject playerLoseLifeParticleEffect;
    [SerializeField] GameObject enemyDeathParticleEffect;

    void Awake()
    {
        player = FindObjectOfType<Player>();
        player.LoseLifeAction += OnPlayerLoseLife;

        enemy = FindObjectOfType<Enemy>();
        enemy.DeathAction += OnEnemyDeath;
    }

    void OnPlayerLoseLife()
    {
        var vfx = Instantiate(playerLoseLifeParticleEffect, transform);
        vfx.transform.position = player.transform.position;

        var particleEffect = vfx.GetComponent<ParticleEffect>();
        particleEffect.ParticleSystem.SetVector4("ParticleColour", player.shipData.UIColour.value);
        particleEffect.enabled = true;
    }

    void OnEnemyDeath()
    {
        var vfx = Instantiate(enemyDeathParticleEffect, transform);
        vfx.transform.position = enemy.transform.position;

        var particleEffect = vfx.GetComponent<ParticleEffect>();
        particleEffect.ParticleSystem.SetVector4("ParticleColour", enemy.shipData.UIColour.value);
        particleEffect.enabled = true;
    }
}