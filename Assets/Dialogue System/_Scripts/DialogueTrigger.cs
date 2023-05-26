using System;
using System.Collections;
using System.Collections.Generic;
using uj.GameManagement;
using UnityEngine;
//using UnityEngine.EventSystems;

public class DialogueTrigger : MonoBehaviour
{

    [Header("Trigger Behavior")]
    public bool TriggerByProximity = true;
    private int dialogueIndexToPlay = 0;
    [Header("Dialogues and Sequence Behavior")]
    public SequenceBehavior sequenceBehavior;
    public DialogueSO[] dialogues;

    //public EventTrigger.TriggerEvent onFinishCallback;

    private void OnTriggerEnter(Collider other)
    {
        if (!TriggerByProximity) return;
        if (other.tag.Equals("Player"))
        {
            TriggerDialogue();

        }
    }

    public void TriggerDialogue()        
    {
        if (!DialoguePlayer.Instance.isPlayingDialogue)
        {
            DialoguePlayer.Instance.StartDialogue(dialogues[dialogueIndexToPlay], this);
        }
    }

    public void TriggerDialogue(int idx)
    {
        if (!DialoguePlayer.Instance.isPlayingDialogue)
        {
            DialoguePlayer.Instance.StartDialogue(dialogues[idx], this);
        }
    }

    public void OnFinishedDialogue(DialogueSO dialogue)
    {
        

        if(dialogue.itemsToDropOnFinish.Length > 0)
        {
            foreach (GameObject item in dialogue.itemsToDropOnFinish)
            {
                Debug.Log(item);
            }
        }

        HandleSequence();
    }

    private void HandleSequence()
    {
        switch (sequenceBehavior)
        {
            case SequenceBehavior.Repeat:
                return;
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
