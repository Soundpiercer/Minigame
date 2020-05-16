using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Minigame
{
    public class TitleScene : MonoBehaviour
    {
        public void ArcherCat()
        {

        }

        public void RandomDice()
        {
            SceneManager.LoadScene("RandomDice");
        }

        public void Archero()
        {

        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}