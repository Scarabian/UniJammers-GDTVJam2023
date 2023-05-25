using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Conversation", menuName = "Dialogue/Conversation", order = 1)]
public class DialogueSO : ScriptableObject
{
    [System.Serializable]
    public struct Blurp
    {
        public string name;
        public Color color;
        public string text;
    }


    public Blurp[] conversation;
    public bool hasFinished = false;
}
