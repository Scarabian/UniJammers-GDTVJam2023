using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CallbackTest : MonoBehaviour
{
   public void TestFunction(DialogueSO dialogue, GameObject sourceGO, DialogueTrigger dialogueTrigger)
    {
        if (dialogue.itemsToDropOnFinish.Length > 0)
        {
            foreach (GameObject item in dialogue.itemsToDropOnFinish)
            {
                //replace this with houw you want to handle giving items to the player
                Debug.Log(item);
            }
        }

        if(dialogueTrigger.GetDialogueIndexToPlay() == 1 || dialogue.title == "Quest for Staff")
        {
            //conditional logic can be used to manipulate the flow of game objects with multiple dialogues on them
            // for example we could repeat until the player has met some condition before continue through
            // to the other dialogues
            dialogueTrigger.sequenceBehavior = SequenceBehavior.Repeat;

            //once the condition is met, update the dialogueTrigger form an outside script

            //TODO: make this handle better in a questing context. Perhaps adding a BeforeDialoguePlays event
            // or finding a way to no use a single callback for the entire array of dialogues.
            // Maybe adding in an way to check the primed dialogue and running conditional logic before it's sent
            // of to the player and being able to assign those to the inspect. But likely these things should just 
            // be handled mindfully through outside scripts as those storyline events happen.

        }
    }
}
