using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    [Header("Trigger Behavior")]
    public bool TriggerByProximity = true;
    private int dialogueIndexToPlay = 0;
    [Header("Dialogues and Sequence Behavior")]
    public SequenceBehavior sequenceBehavior;
    public DialogueSO[] dialogues;

    [Header("What to do When Dialogue Finishes")]
    public DialogueEvents OnFinish;

    private void OnTriggerEnter(Collider other)
    {
        if (!TriggerByProximity) return;
        if (other.tag.Equals("Player"))
        {
            TriggerDialogue();

        }
    }

    //Play whatever dialogue the dialogueIndexToPlay is pointing to
    public void TriggerDialogue()        
    {
        if (!DialoguePlayer.Instance.GetIsPlayingDialogue())
        {
            DialoguePlayer.Instance.StartDialogue(dialogues[dialogueIndexToPlay], this);
        }
    }

    public int GetDialogueIndexToPlay()
    {
        return dialogueIndexToPlay;
    }


    //Use this public method to manually play a known index from code
    public void TriggerDialogue(int idx)
    {
        if (!DialoguePlayer.Instance.GetIsPlayingDialogue())
        {
            DialoguePlayer.Instance.StartDialogue(dialogues[idx], this);
        }
    }

    //Called by DialoguePlayer singleton when dialogue has finished on the object that originally started the dialogue
    public void OnFinishedDialogue(DialogueSO dialogue)
    {
        //doesn't do anything on its own, can be expanded later to change playback functionality
        dialogue.hasFinished = true;

        OnFinish.Invoke(dialogue, this.gameObject, this);

        HandleSequence();

    }

    //Handles what dialogue should be played next
    private void HandleSequence()
    {
        switch (sequenceBehavior)
        {
            case SequenceBehavior.Repeat:
                break;
            case SequenceBehavior.Next:
                dialogueIndexToPlay = (dialogueIndexToPlay < dialogues.Length - 1) ? dialogueIndexToPlay + 1 : dialogueIndexToPlay;
                break;
            case SequenceBehavior.Loop:
                dialogueIndexToPlay = (dialogueIndexToPlay < dialogues.Length - 1) ? dialogueIndexToPlay + 1 : 0;
                break;
            case SequenceBehavior.Random:
                dialogueIndexToPlay = UnityEngine.Random.Range(0, dialogues.Length);
                break;
        }
    }
}

public enum SequenceBehavior { Repeat, Random, Next, Loop };
