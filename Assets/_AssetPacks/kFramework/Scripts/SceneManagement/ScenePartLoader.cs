using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace k.SceneManagement
{
    public enum CheckMethod
    {
        Distance,
        Trigger
    }

    public class ScenePartLoader : MonoBehaviour
    {
        Transform player;
        [SerializeField] CheckMethod checkMethod;
        [SerializeField] float loadRange = 10;

        private bool isLoaded;
        private bool shouldLoad;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;

            //check if the scene is already loaded, in case it's already been added in the editor
            if (SceneManager.sceneCount <= 1) return;

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == gameObject.name)
                {
                    isLoaded = true;
                }
            }
        }

        private void Update()
        {
            if(checkMethod == CheckMethod.Distance)
            {
                DistanceCheck();
            }

            else if(checkMethod == CheckMethod.Trigger)
            {
                TriggerCheck();
            }
        }

        private void DistanceCheck()
        {
            if(Vector3.Distance(player.position, transform.position) < loadRange)
            {
                LoadScene();
            } 
            else
            {
                UnloadScene();
            }
        }

        private void LoadScene()
        {
            if (!isLoaded)
            {
                SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
                isLoaded = true;
            }
        }

        private void UnloadScene()
        {
            if (isLoaded)
            {
                SceneManager.UnloadSceneAsync(gameObject.name);
                isLoaded = false;
            }
        }

        private void TriggerCheck()
        {
            if (shouldLoad) LoadScene();
            else UnloadScene();
        }

        #region 2D collision detection
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                shouldLoad = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                shouldLoad = false;
            }
        }
        #endregion

        #region 3D collision detection
        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.CompareTag("Player"))
        //    {
        //        shouldLoad = true;
        //    }
        //}

        //private void OnTriggerExit(Collider other)
        //{
        //    if (other.CompareTag("Player"))
        //    {
        //        shouldLoad = false;
        //    }
        //}
        #endregion
    }
}