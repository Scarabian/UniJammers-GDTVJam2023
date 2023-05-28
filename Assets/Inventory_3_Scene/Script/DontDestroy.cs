using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    //Hold all object that will not get destroy from different levels
    private static GameObject[] persistentObject = new GameObject[5];
    public int objectIndex;
    // Start is called before the first frame update
    void Awake()
    {
        if(persistentObject[objectIndex] == null)
        {
            persistentObject[objectIndex] = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else if(persistentObject[objectIndex] != gameObject)
        {
            Destroy(gameObject);
        }
    }
}
