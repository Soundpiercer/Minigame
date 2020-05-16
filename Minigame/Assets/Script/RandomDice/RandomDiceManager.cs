using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("Stage Control/Enemy")]
    public int spawnAmount;
    public float spawnInterval;
    public float standbyTime;

    [Header("Stage Control/Shooter")]
    public int maximumShooterCount;
    private int shooterCount;

    [Header("Shooter")]
    public GameObject shooterDicePrefab;
    public Transform shooterDiceRoot;

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

    private void CreateEnemy()
    {
        EnemyDiceBehaviour enemy = Instantiate(enemyDicePrefab, path.startPoint, Quaternion.identity, enemyDiceRoot).GetComponent<EnemyDiceBehaviour>();
        enemy.Init(path, offsetPerSecond);
        enemies.Add(enemy);
    }

    public void AddDiceAtRandomLocation()
    {
        if (shooterCount >= maximumShooterCount)
            return;

        float x = Random.Range(-4f, 4f);
        float y = Random.Range(-4f, 4f);

        Instantiate(shooterDicePrefab, new Vector3(x, y, 0), Quaternion.identity, shooterDiceRoot);
        shooterCount++;
    }

    public void Exit()
    {
        SceneManager.LoadScene("Title");
    }
}
