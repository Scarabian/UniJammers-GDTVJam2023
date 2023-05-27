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
    [Header("Settings")]
    [SerializeField]
    private float textSpeed = 0.02f;

    [Header("References")]
    [SerializeField]
    private GameObject dialogueBox;
    [SerializeField]
    private GameObject arrowIcon;
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI speechText;


    //Flags 
    private bool isPlayingDialogue  = false;
    private bool canContinueToNextLine = false;

    //Dialogue refs
    private DialogueSO currentDialogue;
    private int conversationIdx = -1;
    private Coroutine displayLineCoroutine;



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

        //TODO replace this with new input system
        if(canContinueToNextLine && Input.GetKeyDown(KeyCode.Space)) 
        {
            StepConversation();
        }


    }

    public bool GetIsPlayingDialogue()
    {
        return isPlayingDialogue;
    }

    //Starts the dialogue playback loop with a reference to the DialogueTrigger instance that called it.
    public void StartDialogue(DialogueSO dialogue, DialogueTrigger caller)
    {
        if(dialogue.conversation.Length < 1)
        {
            Debug.LogWarning("conversation has a length of 0. Please make sure to populate dialogue");
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

    private IEnumerator DisplayLine(string line)
    {
        canContinueToNextLine = false;
        arrowIcon.SetActive(false);
        speechText.text = "";

        foreach(char letter in line.ToCharArray())
        {            
            speechText.text += letter;
            yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(textSpeed));
        }

        canContinueToNextLine = true;
        arrowIcon.SetActive(true);
    }

    //increments the conversation index and populates the UI, or if at the end of the conversation, it will finish it.
    private void StepConversation()
    {
        if(conversationIdx < currentDialogue.conversation.Length -1)
        {
            conversationIdx++;
            nameText.text = currentDialogue.conversation[conversationIdx].actor.actorName;
            nameText.color = currentDialogue.conversation[conversationIdx].actor.color;

            //speechText.text = currentDialogue.conversation[conversationIdx].text;
            if(displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(DisplayLine(currentDialogue.conversation[conversationIdx].text));

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


/// <summary>
///  Since the time scale is zero when we suspend the game, a normal coroutine will never finish yielding
///  for seconds. To get around this we'll use a custom coroutine utility to wait for real word time
///  independent of the game time.///  
/// </summary>
public static class CoroutineUtil
{
    public static IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
    }
}
