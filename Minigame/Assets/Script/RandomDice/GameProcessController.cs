using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomDice
{
    public class GameProcessController : MonoBehaviour
    {
        [Header("Phase")]
        public int phaseNumber;
        public Phase phase;

        [Header("Stage Control/Shooter")]
        private int shooterCount;

        [Header("Shooter")]
        public GameObject shooterDicePrefab;
        public Transform shooterDiceRoot;

        [Header("Enemy")]
        public GameObject enemyDicePrefab;
        public Transform enemyDiceRoot;

        [Header("Path Function")]
        public PathFunction path;

        private DiceSlot[] diceSlots;

        private const int NUMBER_OF_SLOTS = 15;
        private const float STANDBY_TIME = 1.0f;

#if UNITY_EDITOR
        private void Start()
        {
            Time.timeScale = 3f;
        }
#endif

        // Init. Called only once.
        public void Init()
        {
            RandomDiceManager.Instance.Init();
            InitDiceSlots();
            StartNextPhase();
        }

        private void InitDiceSlots()
        {
            diceSlots = new DiceSlot[NUMBER_OF_SLOTS];

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    diceSlots[3 * i + j] = new DiceSlot(new Vector3(-4 + 2 * i, 2 * j, 0), false);
                }
            }
        }

        // Start Phase. Phase 1 ~
        public void StartNextPhase()
        {
            // Phase 5 (phases[4]) is the end
            if (phaseNumber >= RandomDiceManager.Instance.phases.Length)
                return;

            RandomDiceManager.Instance.CurrentPhase = RandomDiceManager.Instance.phases[phaseNumber];
            phase = RandomDiceManager.Instance.CurrentPhase;
            RandomDiceManager.Instance.enemies = new List<EnemyDiceBehaviour>();
            RandomDiceManager.Instance.PhaseKillCount = 0;

            phaseNumber++;
            StartCoroutine(StartEnemyMovementEnumerator());
        }

        private IEnumerator StartEnemyMovementEnumerator()
        {
            // standby
            yield return new WaitForSeconds(STANDBY_TIME);

            // game start
            int count = 0;

            while (count < phase.spawnAmount)
            {
                CreateEnemy();
                count++;

                yield return new WaitForSeconds(phase.spawnInterval);
            }

            yield break;
        }

        private void CreateEnemy()
        {
            EnemyDiceBehaviour enemy = Instantiate(enemyDicePrefab, path.startPoint, Quaternion.identity, enemyDiceRoot).GetComponent<EnemyDiceBehaviour>();
            enemy.Init(path, phase.offsetPerSecond);
            RandomDiceManager.Instance.enemies.Add(enemy);
        }

        public void AddDiceAtRandomSlot()
        {
            if (shooterCount >= NUMBER_OF_SLOTS)
                return;

            // Find Random Unoccupied Slot
            DiceSlot targetSlot = new DiceSlot();

            while (true)
            {
                int index = Random.Range(0, NUMBER_OF_SLOTS);
                if (diceSlots[index].isOccupied == false)
                {
                    diceSlots[index].isOccupied = true;
                    targetSlot = diceSlots[index];
                    break;
                }
            }

            // Instantiate Shooter Dice and Init
            ShooterDiceBehaviour shooter = Instantiate(shooterDicePrefab, targetSlot.position, Quaternion.identity, shooterDiceRoot).GetComponent<ShooterDiceBehaviour>();
            ShooterDiceProperty property = RandomDiceData.shooterDiceProperties[Random.Range(0, RandomDiceData.shooterDiceProperties.Length)];
            shooter.Init(property);
            shooterCount++;

            // Increase Required SP To Spawn
            RandomDiceManager.Instance.SP -= RandomDiceManager.Instance.RequiredSPToSpawn;
            RandomDiceManager.Instance.RequiredSPToSpawn += 10;
        }
    }
}