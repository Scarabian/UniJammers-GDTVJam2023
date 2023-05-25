using System.Collections;
using System.Collections.Generic;
using uj.GameManagement;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    public DialogueSO dialogue;



    public void StartDialogue(int idx = 0)
    {
        GameManager.Instance.SuspendGame();
    }

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
