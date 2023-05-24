using k.SoundManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace k.SceneManagement
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] float transitionDuration = 1;
        [SerializeField] SoundManager bgmManager;
        Fader fader;

        private void Awake()
        {
            fader = FindObjectOfType<Fader>();
        }

        public void LoadScene(string scene)
        {
            StartCoroutine(FadeAndLoadScene(scene));
        }

        private IEnumerator FadeAndLoadScene(string scene)
        {
            StartCoroutine(bgmManager.FadeOut(transitionDuration));
            yield return StartCoroutine(fader.FadeOut(transitionDuration));
            SceneManager.LoadScene(scene);
        }

        public void LoadSceneAdditive(string scene)
        {
            SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        }

        public void LoadSceneAdditive(int sceneIndex)
        {
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        }

        public void UnloadScene(string scene)
        {
            SceneManager.UnloadSceneAsync(scene);
        }

        public void UnloadScene(int sceneIndex)
        {
            SceneManager.UnloadSceneAsync(sceneIndex);
        }
    }
}