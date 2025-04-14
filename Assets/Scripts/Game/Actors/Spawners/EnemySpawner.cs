using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static CoroutineHelper;

public class EnemySpawner : MonoBehaviour
{
    new Transform transform;

    [SerializeField] TextAsset spawnTextFile;
    [SerializeField] Enemy[] enemyPrefabs;
    [SerializeField] Boss bossPrefab;

    IEnumerator enemySpawnCoroutine;
    public event Action BossSpawnAction;
    List<Enemy> enemies = new();

    void Awake()
    {
        transform = GetComponent<Transform>();

        if (enemySpawnCoroutine != null)
        {
            StopCoroutine(enemySpawnCoroutine);
        }
        enemySpawnCoroutine = SpawnEnemies();
        StartCoroutine(enemySpawnCoroutine);
    }

    IEnumerator SpawnEnemies()
    {
        //pre-spawn boss first
        var boss = Instantiate(bossPrefab, transform.position, transform.rotation);
        yield return null;

        boss.gameObject.SetActive(false);

        if (spawnTextFile != null)
        {
            //parse spawn file
            var splitFile = spawnTextFile.text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            splitFile = splitFile.Where(i => char.IsDigit(i[0])).ToArray();

            List<float> spawnTimes = new();
            List<int> enemyIndexes = new();
            List<float> xPositions = new();
            List<float> yPositions = new();

            foreach (var line in splitFile)
            {
                if (char.IsDigit(line[0]))
                {
                    var splitLine = line.Split(", ", StringSplitOptions.RemoveEmptyEntries);

                    spawnTimes.Add(float.Parse(splitLine[0]));
                    enemyIndexes.Add(int.Parse(splitLine[1]));
                    xPositions.Add(float.Parse(splitLine[2]));
                    yPositions.Add(float.Parse(splitLine[3]));
                }
            }

            if (enemyPrefabs.Length > 0)
            {
                //pre-spawn all enemies
                for (int i = 0; i < enemyIndexes.Count; i++)
                {
                    //make sure enemy index is within spawn array
                    if (enemyIndexes[i] < enemyPrefabs.Length)
                    {
                        var enemy = Instantiate(enemyPrefabs[enemyIndexes[i]], transform);
                        Vector2 pos = new(xPositions[i], yPositions[i]);
                        enemy.transform.SetPositionAndRotation(pos, transform.rotation);

                        enemy.enabled = false;
                        enemy.gameObject.SetActive(false);

                        enemies.Add(enemy);
                    }
                }
            }

            //activate all enemies based on listed spawn time
            for (int i = 0; i < enemies.Count; i++)
            {
                float delay = i > 0 ? spawnTimes[i] - spawnTimes[i - 1] : spawnTimes[0];

                if (delay > 0f)
                {
                    yield return WaitForSeconds(delay);
                }

                enemies[i].gameObject.SetActive(true);
                enemies[i].enabled = true;
            }

            yield return WaitUntil(() => transform.childCount == 0);
        }

        yield return WaitForSeconds(3f);

        //activate boss
        boss.gameObject.SetActive(true);
        BossSpawnAction?.Invoke();
    }
}