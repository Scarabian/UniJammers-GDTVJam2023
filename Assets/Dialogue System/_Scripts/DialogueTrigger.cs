using System.Collections;
using System.Collections.Generic;
using uj.GameManagement;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueSO dialogue;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if(DialoguePlayer.Instance.isPlayingDialogue == false)
            {
                DialoguePlayer.Instance.StartDialogue(dialogue);
            }
            
        }
    }
}
