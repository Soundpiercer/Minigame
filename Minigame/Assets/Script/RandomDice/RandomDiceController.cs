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

        public void EnableNextPhaseButton()
        {
            nextPhaseButton.SetActive(true);
        }

        public void NextPhase()
        {
            nextPhaseButton.SetActive(false);
            gameProcessController.StartNextPhase();
        }

        public void SetTimeScale(int factor)
        {
            Time.timeScale = factor;
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

            addDiceButton.SetActive(RandomDiceManager.Instance.SP >= RandomDiceManager.Instance.RequiredSPToSpawn);
        }

        public void ShowRequiredSPToSpawn(int requiredSPToSpawn)
        {
            requiredSPText.text = "Required SP : " + requiredSPToSpawn;

            addDiceButton.SetActive(RandomDiceManager.Instance.SP >= RandomDiceManager.Instance.RequiredSPToSpawn);
        }
        #endregion
    }
}