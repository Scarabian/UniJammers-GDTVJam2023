using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Conversation", menuName = "Dialogue/Conversation", order = 1)]
public class DialogueSO : ScriptableObject
{
    [System.Serializable]
    public struct Blurp
    {
        public DialogueActorSO actor;
        public string text;
    }


    public Blurp[] conversation;
    public bool hasFinished = false;
    public GameObject[] itemsToDropOnFinish;
}
