using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestParentUI : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.childCount == 0)
        {
            //If the slots empties delete the parent UI
            Destroy(gameObject);
        }
    }
}
