using System;
using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] TextAsset spawnFile;
    [SerializeField] Enemy[] enemyPrefabs;

    [SerializeField] IntObject selectedBossIndex;
    [SerializeField] Boss[] bossPrefabs;

    IEnumerator enemySpawnCoroutine;
    public event Action BossSpawnAction;

    void Awake()
    {
        //to-do: parse spawn file

        if (enemySpawnCoroutine != null)
        {
            StopCoroutine(enemySpawnCoroutine);
        }
        enemySpawnCoroutine = SpawnEnemies();
        StartCoroutine(enemySpawnCoroutine);
    }

    //to-do: pre-spawn all enemies
    IEnumerator SpawnEnemies()
    {
        var boss = Instantiate(bossPrefabs[selectedBossIndex.value], transform.position, transform.rotation);
        yield return null;
        boss.gameObject.SetActive(false);

        yield return WaitForSeconds(2f);
        boss.gameObject.SetActive(true);
        BossSpawnAction?.Invoke();

        yield break;
    }
}