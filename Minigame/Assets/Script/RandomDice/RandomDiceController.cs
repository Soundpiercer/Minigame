﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RandomDice
{
    public class RandomDiceController : MonoBehaviour
    {
        public GameProcessController gameProcessController;
        public GameObject addDiceButton;
        public GameObject nextPhaseButton;

        public Text phaseText;
        public Text SPText;
        public Text requiredSPText;

        private void Start()
        {
            gameProcessController.Init();
        }

        public void AddDice()
        {
            gameProcessController.AddDiceAtRandomSlot();
        }

        public void SetAddDiceButtonActive(bool isActive)
        {
            addDiceButton.SetActive(isActive);
        }

        public void NextPhase()
        {
            nextPhaseButton.SetActive(false);
            gameProcessController.StartNextPhase();
        }

        public void SetNextPhaseButtonActive(bool isActive)
        {
            nextPhaseButton.SetActive(isActive);
        }

        public void SetTimeScale(int factor)
        {
            Time.timeScale = factor;
        }

        public void DiceLevelUp(int diceTypeByInt)
        {
            ShooterDiceType diceType = (ShooterDiceType)diceTypeByInt;
            foreach (ShooterDiceBehaviour shooter in RandomDiceManager.Instance.shooters)
            {
                ShooterDiceProperty newProperty =
                    RandomDiceData.shooterDiceProperties.ToList().Find(s =>
                    s.tier == shooter.property.tier &&
                    s.level == shooter.property.level + 1
                    );

                if (newProperty != null)
                    shooter.Init(newProperty);
            }
        }

        public void Exit()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Title");
        }

        #region From RandomDiceManager
        public void ShowPhaseNumber(int phaseNumber)
        {
            phaseText.text = "Phase " + phaseNumber;
        }

        public void ShowSP(int sp)
        {
            SPText.text = "SP : " + sp;
        }

        public void ShowRequiredSPToSpawn(int requiredSPToSpawn)
        {
            requiredSPText.text = "Required SP : " + requiredSPToSpawn;
        }
        #endregion
    }
}
