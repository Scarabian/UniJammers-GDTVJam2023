using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Actor", menuName = "Dialogue/Actor", order = 1)]
public class DialogueActorSO : ScriptableObject
{
    public string actorName;
    [Tooltip("Don't forget to set the Alpha to 100")]
    public Color color;
    
}
