using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RandomDice
{
    public class RandomDiceController : MonoBehaviour
    {
        public GameProcessController gameProcessController;

        private void Start()
        {
            gameProcessController.Init();
        }

        public void AddDice()
        {
            gameProcessController.AddDiceAtRandomSlot();
        }

        public void Exit()
        {
            SceneManager.LoadScene("Title");
        }
    }
}