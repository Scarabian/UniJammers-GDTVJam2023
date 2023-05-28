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
        [TextArea(5, 20)]
        public string text;
    }

    [Header("OPTIONAL: Title is used as identifier\nTry to keep it short and unique for each Conversation")]
    public string title;
    public Blurp[] conversation;
    [HideInInspector]
    public bool hasFinished = false;
    public GameObject[] itemsToDropOnFinish;
}
