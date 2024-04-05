using UnityEngine;

public class ShipParticleController : MonoBehaviour
{
    Enemy enemy;

    [SerializeField] StringObject enemyDeathParticleName;

    void Awake()
    {
        enemy = FindObjectOfType<Enemy>();
        enemy.DeathAction += OnEnemyDeath;
    }

    void OnEnemyDeath()
    {
        GameObject enemyDeathParticles = VisualEffectPool.Instance.Get(enemyDeathParticleName);
        var particleEffect = enemyDeathParticles.GetComponent<ParticleEffect>();

        enemyDeathParticles.transform.position = enemy.transform.position;
        enemyDeathParticles.SetActive(true);

        particleEffect.ParticleSystem.SetVector4("ParticleColour", enemy.shipData.UIColour.value);
        particleEffect.enabled = true;
    }
}