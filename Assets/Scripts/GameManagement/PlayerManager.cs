using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public int level = 0;
    public float maxHealth = 100f;
    public float maxXP = 100f;
    public float maxLight = 100f;

    public float currentHealth = 100f;
    public float currentXP = 0f;
    public float currentLight = 100f;

    public float lowHealthThreshold = 30f;
    public float flashDuration = 0.2f;

    public Image healthFill;
    public Image xpFill;
    public Image lightFill;
    public Image screenFlash;
    public TextMeshProUGUI levelText;

    public GameObject lightBoltPrefab;
    public Transform firePoint;
    public RenderTextureSwitcher renderTextureSwitcher;
    public bool IsDull;
    private Color originalScreenColor;

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        renderTextureSwitcher = FindObjectOfType<RenderTextureSwitcher>();
    }

    private void Start()
    {
        originalScreenColor = screenFlash.color;
    }

    private void Update()
    {
        UpdateHealthUI();
        UpdateXPUI();
        UpdateLightUI();

        CheckLowHealth();
    }

    private void UpdateHealthUI()
    {
        float healthRatio = currentHealth / maxHealth;
        healthFill.fillAmount = healthRatio;
       
    }

    private void UpdateXPUI()
    {
        levelText.text =   "Level : " + level;


        float xpRatio = currentXP / maxXP;
        xpFill.fillAmount = xpRatio;
    }

    private void UpdateLightUI()
    {
        
        float lightRatio = currentLight / maxLight;
        lightFill.fillAmount = lightRatio;
    }

    private void CheckLowHealth()
    {
        if (currentHealth <= lowHealthThreshold)
        {
            // Flash the screen
            FlashScreen();
        }
    }

    private void FlashScreen()
    {
        StartCoroutine(ScreenFlashRoutine());
    }

    private System.Collections.IEnumerator ScreenFlashRoutine()
    {
        screenFlash.color = Color.white;

        yield return new WaitForSeconds(flashDuration);

        screenFlash.color = originalScreenColor;
    }

    public void OnRandomRangeAttack(InputAction.CallbackContext context)
    {
        if (context.performed && renderTextureSwitcher.getIsDull())
        {
            float attackRange = Random.Range(20f, 30f);
            Instantiate(lightBoltPrefab, firePoint.position, firePoint.rotation);


            //Debug.Log("Performing random range attack at " + attackRange + " meters.");
        }
    }

    public void OnMeleeAttack(InputAction.CallbackContext context)
    {
        if (context.performed && renderTextureSwitcher.getIsDull())
        {
            Debug.Log("Performing melee attack within 5 meters.");
        }
    }

    public void OnMidRangeAttack(InputAction.CallbackContext context)
    {
        if (context.performed && renderTextureSwitcher.getIsDull()  )
        {
            float attackRange = Random.Range(5f, 20f);
            Debug.Log("Performing mid-range attack at " + attackRange + " meters.");
        }
    }
}