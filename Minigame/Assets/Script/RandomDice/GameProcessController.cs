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

        public void Init()
        {
            BuildStage();

            InitDiceSlots();
            InitRandomDiceManager();

            StartCoroutine(GameProcessEnumerator());
        }

        public void InitNextPhase()
        {
            if (phaseNumber < RandomDiceManager.Instance.phases.Length)
            {
                BuildStage();
                phaseNumber++;

                StartCoroutine(GameProcessEnumerator());
            }
        }

        private void InitRandomDiceManager()
        {
            RandomDiceManager.Instance.CurrentPhase = phase;

            RandomDiceManager.Instance.SP = 100;
            RandomDiceManager.Instance.RequiredSPToSpawn = 10;
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

        private void BuildStage()
        {
            phase = RandomDiceManager.Instance.phases[phaseNumber];
            phaseNumber = phase.phaseNumber;

            RandomDiceManager.Instance.enemies = new List<EnemyDiceBehaviour>();
        }

        private IEnumerator GameProcessEnumerator()
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

            // Instantiate Shooter Dice
            Instantiate(shooterDicePrefab, targetSlot.position, Quaternion.identity, shooterDiceRoot);
            shooterCount++;

            // Increase Required SP To Spawn
            RandomDiceManager.Instance.SP -= RandomDiceManager.Instance.RequiredSPToSpawn;
            RandomDiceManager.Instance.RequiredSPToSpawn += 10;
        }
    }
}