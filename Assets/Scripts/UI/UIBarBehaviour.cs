using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBarBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Image currentHealthFill;
    [SerializeField] private Image currentLightFill;

    private int divideFillAmount = 10;

    private void Update()
    {
        currentHealthFill.fillAmount = playerMovement.playerCurrentHealth / divideFillAmount;

        currentLightFill.fillAmount = playerMovement.playerCurrentLight / divideFillAmount;
    }
}
