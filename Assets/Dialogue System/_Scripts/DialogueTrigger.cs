using System;
using System.Collections;
using System.Collections.Generic;
using uj.GameManagement;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    [Header("Trigger Behavior")]
    public bool TriggerByProximity = true;
    private int dialogueIndexToPlay = 0;
    [Header("Dialogues and Sequence Behavior")]
    public SequenceBehavior sequenceBehavior;
    public DialogueSO[] dialogues;

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
        if (!DialoguePlayer.Instance.isPlayingDialogue)
        {
            DialoguePlayer.Instance.StartDialogue(dialogues[dialogueIndexToPlay], this);
        }
    }


    //Use this public method to manually play a known index from code
    public void TriggerDialogue(int idx)
    {
        if (!DialoguePlayer.Instance.isPlayingDialogue)
        {
            DialoguePlayer.Instance.StartDialogue(dialogues[idx], this);
        }
    }

    //Called by DialoguePlayer singleton when dialogue has finished on the object that originally started the dialogue
    public void OnFinishedDialogue(DialogueSO dialogue)
    {
        

        if(dialogue.itemsToDropOnFinish.Length > 0)
        {
            foreach (GameObject item in dialogue.itemsToDropOnFinish)
            {
                //TODO replace this with behavior to give items. Items is of type Gameobject
                Debug.Log(item);
            }
        }

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
