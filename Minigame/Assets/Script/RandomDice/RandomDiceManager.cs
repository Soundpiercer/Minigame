using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomDice
{
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

        public RandomDiceController controller;

        public Phase[] phases;
        public List<ShooterDiceBehaviour> shooters = new List<ShooterDiceBehaviour>();
        public Dictionary<ShooterDiceType, int> shooterLevels = new Dictionary<ShooterDiceType, int>();
        public List<EnemyDiceBehaviour> enemies = new List<EnemyDiceBehaviour>();

        private const int INIT_SP = 100;
        private const int INIT_REQUIRED_SP_TO_SPAWN = 10;
        private const int MINIMUM_LEVEL = 1;
        private const int MAXIMUM_LEVEL = 5;
        private const int REQUIRED_SP_TO_LEVELUP = 100;

        public void Init()
        {
            phases = RandomDiceData.phases;
            foreach (string s in Enum.GetNames(typeof(ShooterDiceType)))
            {
                shooterLevels.Add((ShooterDiceType)Enum.Parse(typeof(ShooterDiceType), s), MINIMUM_LEVEL);
            }

            SP = INIT_SP;
            RequiredSPToSpawn = INIT_REQUIRED_SP_TO_SPAWN;
        }

        public void DiceLevelUp(ShooterDiceType diceType)
        {
            // if the dice already reached the maximum level, return
            if (shooterLevels[diceType] == MAXIMUM_LEVEL)
                return;

            // Minimum required SP is 100
            if (SP < REQUIRED_SP_TO_LEVELUP)
                return;

            SP -= REQUIRED_SP_TO_LEVELUP;

            // Level Up to managed data
            shooterLevels[diceType] = Mathf.Clamp(shooterLevels[diceType] + 1, MINIMUM_LEVEL, MAXIMUM_LEVEL);

            // Level Up currently active shooter dice
            foreach (ShooterDiceBehaviour shooter in shooters)
            {
                if (shooter.property.shooterDiceType != diceType)
                    continue;

                ShooterDiceProperty property =
                    RandomDiceData.shooterDiceProperties.ToList().Find(s =>
                    s.shooterDiceType == shooter.property.shooterDiceType &&
                    s.tier == shooter.property.tier &&
                    s.level == shooterLevels[diceType]
                    );

                if (property != null)
                    shooter.Init(property);
            }
        }

        private int sp;
        public int SP
        {
            get { return sp; }
            set
            {
                sp = value;
                controller.ShowSP(sp);
                controller.SetAddDiceButtonActive(SP >= RequiredSPToSpawn);
            }
        }
        private int requiredSPToSpawn;
        public int RequiredSPToSpawn
        {
            get { return requiredSPToSpawn; }
            set
            {
                requiredSPToSpawn = value;
                controller.ShowRequiredSPToSpawn(requiredSPToSpawn);
                controller.SetAddDiceButtonActive(SP >= RequiredSPToSpawn);
            }
        }
        private Phase currentPhase;
        public Phase CurrentPhase
        {
            get { return currentPhase; }
            set
            {
                currentPhase = value;
                controller.ShowPhaseNumber(currentPhase.phaseNumber);
            }
        }
        private int phaseKillCount;
        public int PhaseKillCount
        {
            get { return phaseKillCount; }
            set
            {
                phaseKillCount = value;

                // don't enable next phase when
                // player haven't killed all enemies
                // player already reached the last phase
                if (phaseKillCount >= CurrentPhase.spawnAmount && CurrentPhase.phaseNumber < phases.Length)
                {
                    controller.SetNextPhaseButtonActive(true);
                }
            }
        }
    }
}