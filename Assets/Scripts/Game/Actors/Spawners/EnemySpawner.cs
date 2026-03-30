using System;
using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class EnemySpawner : MonoBehaviour
{
    new Transform transform;

    [SerializeField] IntObject selectedBossIndex;
    [SerializeField] GameObject[] bossPrefabs;

    IEnumerator enemySpawnCoroutine;
    public event Action BossSpawnAction;

    void Awake()
    {
        transform = GetComponent<Transform>();

        if (enemySpawnCoroutine != null)
        {
            StopCoroutine(enemySpawnCoroutine);
        }
        enemySpawnCoroutine = SpawnBoss();
        StartCoroutine(enemySpawnCoroutine);
    }

    IEnumerator SpawnBoss()
    {
        //pre-spawn boss first
        var boss = Instantiate(bossPrefabs[selectedBossIndex.value], transform.position, transform.rotation);
        yield return null;

        boss.gameObject.SetActive(false);

        yield return WaitForSeconds(3f);
        
        //activate boss
        boss.gameObject.SetActive(true);
        BossSpawnAction?.Invoke();
    }
}