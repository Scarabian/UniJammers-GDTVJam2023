using System.Collections;
using System.Collections.Generic;
using uj.GameManagement;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool isProximity = true;
    public DialogueSO dialogue;
    private void OnTriggerEnter(Collider other)
    {
        if (!isProximity) return;
        if (other.tag.Equals("Player"))
        {
            TriggerDialogue();
            
        }
    }

    public void TriggerDialogue()
    {
        if (!DialoguePlayer.Instance.isPlayingDialogue)
        {
            DialoguePlayer.Instance.StartDialogue(dialogue);
        }
    }
}
