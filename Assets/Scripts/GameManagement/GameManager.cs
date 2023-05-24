using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using k.SceneManagement;
using uj.input;

namespace uj.GameManagement
{
    public class GameManager : MonoBehaviour
    {
        InputReader inputReader;

        Fader fader;
        [SerializeField] GameObject pauseScreen;

        bool isPaused = false;

        void Start()
        {
            fader = FindObjectOfType<Fader>();
            inputReader = FindObjectOfType <InputReader>();

            if (fader != null)
                StartCoroutine(fader.FadeIn(1));
        }

        private void Update()
        {
            if (inputReader.GetPauseButtonPressed())
            {
                if (!isPaused)
                    PauseGame();
                else
                    UnPauseGame();
            }
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
            isPaused = true;
            pauseScreen.SetActive(true);
        }

        public void UnPauseGame()
        {
            Time.timeScale = 1;
            isPaused = false;
            pauseScreen.SetActive(false);
        }
    }
}