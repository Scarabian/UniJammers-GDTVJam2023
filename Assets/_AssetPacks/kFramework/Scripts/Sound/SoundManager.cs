using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace k.SoundManagement
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] List<SoundClip> soundClips;

        AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(string clipName)
        {
            SoundClip soundClip = soundClips.Find(s => s.name == clipName);

            audioSource.clip = soundClip.clip;
            audioSource.volume = soundClip.volume;
            audioSource.loop = soundClip.loop;

            if (soundClip.delay <= 0)
                audioSource.Play();
            else audioSource.PlayDelayed(soundClip.delay);
        }

        //public void FadeInAudioClip(string clipName, float fadeTime)
        //{
        //    StartCoroutine(FadeInClip(clipName, fadeTime));
        //}

        public IEnumerator FadeInClip(string clipName, float fadeTime)
        {
            SoundClip soundClip = soundClips.Find(s => s.name == clipName);

            audioSource.clip = soundClip.clip;
            audioSource.volume = 0;
            audioSource.loop = soundClip.loop;

            audioSource.Play();
            float timer = 0;

            while (audioSource.volume < soundClip.volume)
            {
                timer += Time.unscaledDeltaTime / fadeTime;
                audioSource.volume = Mathf.Lerp(0, soundClip.volume, timer);
                yield return new WaitForEndOfFrame();
            }
        }

        //public void FadeOutCurrentClip(float fadeTime)
        //{
        //    StartCoroutine(FadeOut(fadeTime));
        //}

        public IEnumerator FadeOut(float fadeTime)
        {
            float startVolume = audioSource.volume;
            float timer = 0;

            while (audioSource.volume > 0)
            {
                timer += Time.unscaledDeltaTime / fadeTime;
                audioSource.volume = Mathf.Lerp(startVolume, 0, timer);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}