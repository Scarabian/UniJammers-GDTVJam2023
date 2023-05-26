using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Actor", menuName = "Dialogue/Actor", order = 1)]
public class DialogueActorSO : ScriptableObject
{
    public string actorName;
    [Header("Rember to set the alpha to max")]
    public Color color;
    
}
