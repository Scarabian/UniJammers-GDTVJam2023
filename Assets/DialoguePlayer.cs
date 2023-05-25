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


    public static DialoguePlayer Instance;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

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

    public void StartDialogue(DialogueSO dialogue)
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
            isPlayingDialogue= true;
            GameManager.Instance.SuspendGame();
            currentDialogue = dialogue;
            dialogueBox.SetActive(true);
            StepConversation();
        }
    }

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

    private void FinishConversation()
    {
        isPlayingDialogue = false;
        currentDialogue.hasFinished = true;
        conversationIdx = -1;
        dialogueBox.SetActive(false);
        GameManager.Instance.UnsuspendGame();
    }
}
