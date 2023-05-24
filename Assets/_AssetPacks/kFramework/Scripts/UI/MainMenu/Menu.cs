using UnityEngine;
using k.SceneManagement;
using TMPro;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine.UI;

namespace k.UI.MainMenu
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] string firstScene;
        LevelLoader levelLoader;

        [Header("Options screen")]
        [SerializeField] TMP_Dropdown resolutionDropdown;
        Resolution[] resolutions;
        [SerializeField] Slider masterSlider;
        [SerializeField] Slider sfxSlider;
        [SerializeField] Slider musicSlider;

        [SerializeField] AudioMixer audioMixer;



        private void Start()
        {
            levelLoader = FindObjectOfType<LevelLoader>();
            SetupResolution();
            SetupSliders();
        }

        public void PlayGame()
        {
            levelLoader.LoadScene(firstScene);
            this.enabled = false;
        }

        public void QuitGame()
        {
            Debug.Log("Quitting...");
            Application.Quit();
        }

        private void SetupResolution()
        {
            resolutions = Screen.resolutions;

            resolutionDropdown.ClearOptions();

            int currentResolutionIndex = 0;
            List<string> options = new List<string>();

            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = $"{resolutions[i].width} x {resolutions[i].height} @ {resolutions[i].refreshRateRatio}";
                options.Add(option);
                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }

        private void SetupSliders()
        {
            audioMixer.GetFloat("MasterVolume", out float masterValue);
            masterSlider.value = masterValue;

            audioMixer.GetFloat("SfxVolume", out float sfxValue);
            sfxSlider.value = sfxValue;

            audioMixer.GetFloat("MusicVolume", out float musicValue);
            musicSlider.value = musicValue;
        }

        public void SetResolution(int resolutionIndex)
        {
            Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
        }

        public void SetMasterVolume(float volume)
        {
            audioMixer.SetFloat("MasterVolume", volume);
        }

        public void SetSfxVolume(float volume)
        {
            audioMixer.SetFloat("SfxVolume", volume);
        }

        public void SetMusicVolume(float volume)
        {
            audioMixer.SetFloat("MusicVolume", volume);
        }

        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }
    }
}