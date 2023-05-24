using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace k.SoundManagement
{
    [System.Serializable]
    public class SoundClip
    {
        public string name;
        public AudioClip clip;
        public bool loop = false;
        [Range(0, 1)] public float volume = 0.5f;
        [Range(-3, 3)] public float pitch = 1;
        public float delay = 0;
    }
}