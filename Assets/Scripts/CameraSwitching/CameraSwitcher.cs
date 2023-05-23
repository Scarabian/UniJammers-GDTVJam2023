using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
  public RectTransform Camera1RTStorage;
  public RectTransform Camera2RTStorage;

  public GameObject Camera1GO;
  public GameObject Camera2GO;
  void Awake()
    {
    //Switch Test
    Camera1GO.GetComponent<RectTransform>().sizeDelta = Camera2RTStorage.sizeDelta;
    Camera2GO.GetComponent<RectTransform>().sizeDelta = Camera1RTStorage.sizeDelta;
    Camera2GO.GetComponent<RectTransform>().position = Camera1RTStorage.position;
    Camera1GO.GetComponent<RectTransform>().position = Camera2RTStorage.position;
  }

    // Update is called once per frame
    void Update()
    {
        
    }
}
