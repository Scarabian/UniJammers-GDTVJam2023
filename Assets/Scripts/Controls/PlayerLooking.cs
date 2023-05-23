using System.Collections;
using System.Collections.Generic;
using uj.input;
using UnityEngine;

public class PlayerLooking : MonoBehaviour
{

  InputReader inputReader;

  public Camera cameraController;

  Vector2 lookInput;


  // Start is called before the first frame update
  void Start()
    {
    inputReader = FindObjectOfType<InputReader>(); 
  }

    // Update is called once per frame
    void Update()
    {
    lookInput = inputReader.GetLookInput();
    this.transform.rotation = Quaternion.Euler(0, lookInput.y, 0); ;
    cameraController.transform.localRotation = Quaternion.Euler(lookInput.x, 0, 0);
  }
}
