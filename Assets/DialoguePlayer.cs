using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;
using uj.GameManagement;

public class DialoguePlayer : MonoBehaviour
{


    [SerializeField]
    private GameObject dialogueBox;

    public bool isPlayingDialogue = false;

    private DialogueSO currentDialogue;
    private int conversationIdx;
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
            StepConversation(0);
        }
    }

    public void StepConversation(int idx)
    {
        nameText.text = currentDialogue.conversation[idx].name;
        nameText.color = currentDialogue.conversation[idx].color;
        speechText.text = currentDialogue.conversation[idx].text;
    }
}
