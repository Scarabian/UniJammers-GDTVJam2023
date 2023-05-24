using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace k.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        //[SerializeField] float fadeDuration = 1;
        Image fader;

        private void Awake()
        {
            fader = GetComponent<Image>();
        }

        public IEnumerator FadeOut(float fadeDuration)
        {
            Color curColor = fader.color;
            float duration = 0;

            while (fader.color.a < 1)
            {
                curColor.a = Mathf.Lerp(0, 1, duration += 1 / fadeDuration * Time.unscaledDeltaTime);

                fader.color = curColor;

                yield return null;
            }
        }

        public IEnumerator FadeIn(float fadeDuration)
        {
            Color curColor = fader.color;
            float duration = 0;

            while (fader.color.a > 0)
            {
                curColor.a = Mathf.Lerp(1, 0, duration += 1 / fadeDuration * Time.unscaledDeltaTime);

                fader.color = curColor;

                yield return null;
            }
        }
    }
}