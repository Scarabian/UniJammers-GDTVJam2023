using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Conversation", menuName = "Dialogue/Conversation", order = 1)]
public class ConversationSO : ScriptableObject
{
    [System.Serializable]
    public struct Blurp
    {
        public string name;
        public string text;
    }


    public List<Blurp> conversation;
}
