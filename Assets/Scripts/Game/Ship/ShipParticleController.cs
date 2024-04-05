using UnityEngine;

public class ShipParticleController : MonoBehaviour
{
    Enemy enemy;

    [SerializeField] GameObject enemyDeathParticleEffect;

    void Awake()
    {
        enemy = FindObjectOfType<Enemy>();
        enemy.DeathAction += OnEnemyDeath;
    }

    void OnEnemyDeath()
    {
        var vfx = Instantiate(enemyDeathParticleEffect, enemy.transform);
        var particleEffect = vfx.GetComponent<ParticleEffect>();

        vfx.SetActive(true);

        particleEffect.ParticleSystem.SetVector4("ParticleColour", enemy.shipData.UIColour.value);
        particleEffect.enabled = true;
    }
}