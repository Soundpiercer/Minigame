using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDiceManager : MonoBehaviour
{
    #region Simple Singleton
    public static RandomDiceManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }
    #endregion

    [Header("Stage Control")]
    public int spawnAmount;
    public float spawnInterval;
    public float standbyTime;

    [Header("Enemy")]
    public GameObject enemyDicePrefab;
    public Transform enemyDiceRoot;

    [Header("Path Function")]
    public PathFunction path;
    public float offsetPerSecond;

    [HideInInspector]
    public List<EnemyDiceBehaviour> enemies = new List<EnemyDiceBehaviour>();

    private void Start()
    {
        StartCoroutine(GameProcessEnumerator());
    }

    private IEnumerator GameProcessEnumerator()
    {
        // standby
        yield return new WaitForSeconds(standbyTime);

        // game start
        int count = 0;

        while (count < spawnAmount)
        {
            CreateEnemy();
            count++;

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void CreateEnemy()
    {
        EnemyDiceBehaviour enemy = Instantiate(enemyDicePrefab, path.startPoint, Quaternion.identity, enemyDiceRoot).GetComponent<EnemyDiceBehaviour>();
        enemy.Init(path, offsetPerSecond);
        enemies.Add(enemy);
    }
}
