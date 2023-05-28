using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallbackTest : MonoBehaviour
{
   public void TestFunction(DialogueSO dialogue)
    {
        if (dialogue.itemsToDropOnFinish.Length > 0)
        {
            foreach (GameObject item in dialogue.itemsToDropOnFinish)
            {
                Debug.Log(item);
            }
        }
    }
}
