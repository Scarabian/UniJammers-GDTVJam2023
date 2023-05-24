using UnityEngine;
using UnityEngine.UI;

public class LightWeapon : MonoBehaviour
{
    public Light weaponLight;
    public float maxIntensity = 10f;
    public float damageMultiplier = 0.2f;
    public float absorbMultiplier = 0.5f; // Determines how much light is absorbed from enemies
    public Slider healthSlider;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    private void Attack()
    {
        // Reduce the intensity of the light
        weaponLight.intensity -= damageMultiplier;

        // Calculate the remaining light as a percentage
        float percentage = (weaponLight.intensity / maxIntensity) * 100f;

        // Update the UI slider value with the percentage
        healthSlider.value = percentage;

        // Check if the light is depleted
        if (weaponLight.intensity <= 0f)
        {
            weaponLight.intensity = 0f;
            Debug.Log("Weapon light depleted!");
        }
    }

    public void AbsorbLight(float amount)
    {
        // Increase the intensity of the light
        weaponLight.intensity += amount * absorbMultiplier;

        // Clamp the intensity to the maximum value
        weaponLight.intensity = Mathf.Clamp(weaponLight.intensity, 0f, maxIntensity);

        // Calculate the remaining light as a percentage
        float percentage = (weaponLight.intensity / maxIntensity) * 100f;

        // Update the UI slider value with the percentage
        healthSlider.value = percentage;
    }

    public void DrainLight(float amount)
    {
        // Decrease the intensity of the light
        weaponLight.intensity -= amount;

        // Calculate the remaining light as a percentage
        float percentage = (weaponLight.intensity / maxIntensity) * 100f;

        // Update the UI slider value with the percentage
        healthSlider.value = percentage;

        // Check if the light is depleted
        if (weaponLight.intensity <= 0f)
        {
            weaponLight.intensity = 0f;
            Debug.Log("Player light depleted!");
        }
    }
}
