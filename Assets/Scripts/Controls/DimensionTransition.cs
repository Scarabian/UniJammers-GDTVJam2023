using k.SoundManagement;
using uj.input;
using UnityEngine;
using UnityEngine.UI;

public class RenderTextureSwitcher : MonoBehaviour
{
    [Header("Screen Switch")]
    [SerializeField] private Camera fPCam;   //first parent camera selectebale
    [SerializeField] private Camera tDCam;   //Top Down camera selectable
    [SerializeField] private RawImage smallScreenRawImage;  // RawImage component to display the camera output
    [SerializeField] private RawImage largeScreenRawImage;  // RawImage component to display the camera output
    [SerializeField] private Material monoMaterial; // image compnent to apply to the mono camera
    [SerializeField] private Image recticle;
    [SerializeField] private SoundManager soundManager;

    private bool isDull = false;
   

    InputReader inputReader;

    [Header("UI Switch")]
    [SerializeField] private Image rightCameraUI; //Inventory Image Holder
    [SerializeField] private Image lightBar; //LightBar Image Holder
    [SerializeField] private Image healthBar; //HealthBar Image Holder
    [SerializeField] private PlayerManager playerManager;
    private Color color;
    public bool isLightMode;
    public float dullDrainLight = 0.25f;
    public float brightGainLight = 0.5f;
    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("soundManager").GetComponent<SoundManager>();
    }
    private void Start()
    {
        //Start in Light Dimension
        isLightMode = true;

        // Make sure at least one camera and RawImage are assigned
        if (fPCam == null || tDCam == null)
        {
            Debug.LogError("A camera is not assigned");
            return;

        }

        if (smallScreenRawImage == null || largeScreenRawImage == null)
        {
            Debug.LogError("An image is not assigned");
            return;

        }

        inputReader = FindObjectOfType<InputReader>();

        //References
        rightCameraUI = GameObject.Find("RightCameraUI").GetComponent<Image>();
        lightBar = GameObject.Find("LightBarFill").GetComponent<Image>();
        healthBar = GameObject.Find("HealthBarFill").GetComponent<Image>();
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();

        soundManager.FadeInClip("brightFull", 2);

    }

    private void Update()
    {
        LightBar();
        // Check for input to switch camera outputs
        if (inputReader.GetCameraSwitchPressedThisFrame())
        {
            Debug.Log("Switch flipped");
            SwitchScreens();
            SwitchUI();
            
        }
    }

    private void SwitchScreens()
    {
        //Reassign the target Textures to the other camera.
        
        
        if (!isDull)
        {
            isLightMode = false;
            smallScreenRawImage.texture = tDCam.targetTexture;
            largeScreenRawImage.texture = fPCam.targetTexture;
            smallScreenRawImage.material = null;
            largeScreenRawImage.material = monoMaterial;
        }
        else
        {
            isLightMode = true;
            smallScreenRawImage.texture = fPCam.targetTexture;
            largeScreenRawImage.texture = tDCam.targetTexture;
            smallScreenRawImage.material = monoMaterial;
            largeScreenRawImage.material = null;
        }

        toggleIsDull();
        
        //Debug.Log("AFTER small = " + smallScreenRawImage.texture + "Large = " + largeSCreenRawImage.texture + "\nfpCam = " + fPCam.targetTexture + "tDCam = " + tDCam.targetTexture);
    }

    private void SwitchUI()
    {
        if (!isLightMode)
        {
            //Inventory Frame
            ColorUtility.TryParseHtmlString("#4D4242", out color);
            rightCameraUI.color = color; 

            //Health Bar Fill
            ColorUtility.TryParseHtmlString("#968F0B", out color);
            lightBar.color = color; 

            //Light Bar Fill
            ColorUtility.TryParseHtmlString("#B76C6C", out color);
            healthBar.color = color; 
        }
        else
        {
            //Revert the color back to default
            ColorUtility.TryParseHtmlString("#FFFFFF", out color);
            rightCameraUI.color = color;
            lightBar.color = color;  
            healthBar.color = color; 
        }
    }

    // ------- REDUCE CURRENT LIGHT ------- //
    public void LightBar()
    {
        //In Light Dimension & Light is not Full
        if(isLightMode && playerManager.currentLight < 100)
        {
            playerManager.currentLight += brightGainLight *  Time.deltaTime;
        }


        //In Dull Dimension 
        if(!isLightMode && playerManager.currentLight > 0 )
        {
            playerManager.currentLight -= dullDrainLight * Time.deltaTime;
        }
        else
        {
            SwitchUI();
            smallScreenRawImage.texture = fPCam.targetTexture;
            largeScreenRawImage.texture = tDCam.targetTexture;
            isLightMode = true;
        }
    }
    public  bool getIsDull()
    {
        return isDull;
    }
    private void toggleIsDull()
    {
        isDull= !isDull;
        if (isDull )
        {
            Debug.Log("Sound");
            HideCursor();
            soundManager.PlaySound("dullFull");
        } 
        else
        {
            ShowCursor();
            soundManager.PlaySound("brightFull"); 
        }
    }
    private void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        recticle.enabled = false;
       
    }
    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
       recticle.enabled= true;
    }

 }



    

