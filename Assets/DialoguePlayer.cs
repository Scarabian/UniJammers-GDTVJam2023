using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;
using uj.GameManagement;
using System;

public class DialoguePlayer : MonoBehaviour
{


    [SerializeField]
    private GameObject dialogueBox;

    public bool isPlayingDialogue = false;

    private DialogueSO currentDialogue;
    private int conversationIdx = -1;
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI speechText;

    //this is super ugly and I should propbably just be using events instead of a callback structure
    private DialogueTrigger triggerThatStartedDialogue;


    public static DialoguePlayer Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (!isPlayingDialogue) return;

        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            StepConversation();
        }


    }

    //Starts the dialogue playback loop with a reference to the DialogueTrigger instance that called it.
    public void StartDialogue(DialogueSO dialogue, DialogueTrigger caller)
    {
        if(dialogue.conversation.Length < 1)
        {
            Debug.LogWarning("conversation has a count of 0. Please make sure to populate dialogue");
            return;
        }
        if(isPlayingDialogue)
        {
            Debug.LogWarning("Tried to start dialogue while dialogue is already running");
            return;
        }
        else
        {
            triggerThatStartedDialogue= caller;
            isPlayingDialogue= true;
            GameManager.Instance.SuspendGame();
            currentDialogue = dialogue;
            dialogueBox.SetActive(true);
            StepConversation();
        }
    }

    //increments the conversation index and populates the UI, or if at the end of the conversation, it will finish it.
    private void StepConversation()
    {
        if(conversationIdx < currentDialogue.conversation.Length -1)
        {
            conversationIdx++;
            nameText.text = currentDialogue.conversation[conversationIdx].actor.actorName;
            nameText.color = currentDialogue.conversation[conversationIdx].actor.color;
            speechText.text = currentDialogue.conversation[conversationIdx].text;

        }
        else
        {
            FinishConversation();
        }
    }

    //run callback on object that started the dialogue then reset the dialogue player and unsuspend the game
    private void FinishConversation()
    {
        triggerThatStartedDialogue.OnFinishedDialogue(currentDialogue);        
        isPlayingDialogue = false;
        currentDialogue.hasFinished = true;
        conversationIdx = -1;
        dialogueBox.SetActive(false);
        GameManager.Instance.UnsuspendGame();
        currentDialogue = null;

    }
}
