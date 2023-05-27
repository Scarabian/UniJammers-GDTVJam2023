using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Actor", menuName = "Dialogue/Actor", order = 1)]
public class DialogueActorSO : ScriptableObject
{
    public string actorName;
    [Header("Rember to set the alpha to max")]
    public Color color;

    [Header("Audio Profile")]
    public AudioClip[] dialogueTypingSoundClips;
    public bool stopAudioSource;

    [Range(1, 5)]
    public int characterFrequency = 2;
    [Range(-3, 3)]
    public float minPitch = 0.5f;
    [Range(-3, 3)]
    public float maxPitch = 3f;

}
