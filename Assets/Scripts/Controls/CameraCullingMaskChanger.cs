using UnityEngine;

public class CameraCullingMaskChanger : MonoBehaviour
{

    public Camera mainCamera; // The camera whose culling mask you want to change    
    public Camera subCamera; // The camera whose culling mask you want to change

    private int swapMaskMain;
    private int swapMaskSub;
    private int holdMask;

    private string holdlayer;

    public bool isFirstPersonLargeScreen = false;


    private void Start()
    {
        swapMaskMain = mainCamera.cullingMask;
        swapMaskSub = subCamera.cullingMask;


    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleCullingMask();

        }
    }

    // Method to change the culling mask
    void ToggleCullingMask()
    {
        // Debug.Log(mainCamera.cullingMask);
        /* int allLayers = ~0; // Bitmask for all layers
         int excludedLayerMask = 1 << LayerMask.NameToLayer(excludedLayer);
         int newCullingMask = allLayers & (~excludedLayerMask);
        */
       // mainCamera.orthographic = !mainCamera.orthographic;
        //subCamera.orthographic = !subCamera.orthographic;
        mainCamera.cullingMask = swapMaskSub;
        subCamera.cullingMask = swapMaskMain;
        holdMask = swapMaskSub;
        swapMaskSub = swapMaskMain;
        swapMaskMain = holdMask;
        isFirstPersonLargeScreen = !isFirstPersonLargeScreen;
    }
}
