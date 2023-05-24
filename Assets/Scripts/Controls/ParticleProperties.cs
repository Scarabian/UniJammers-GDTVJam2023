using UnityEngine;

public class ParticleProperties : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public float newEmissionRate = 300f; // New emission rate value
    public float newLifetime = 1.2f; // New lifetime value

    private ParticleSystem.EmissionModule emissionModule;
    private ParticleSystem.MainModule mainModule;
    

    private void Start()
    {
        // Get the Emission and Main modules of the Particle System
        emissionModule = particleSystem.emission;
        mainModule = particleSystem.main;


        // Modify the emission rate and lifetime


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
         //   Debug.Log("Currentrate" + emissionModule.rateOverTime.constant);
            newEmissionRate -= 25f;
            newLifetime -= .1f;
            float rate =emissionModule.rateOverTime.constant; 
            emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(newEmissionRate);
            mainModule.startLifetime = new ParticleSystem.MinMaxCurve(newLifetime);
            if(emissionModule.rateOverTime.constant <=0)
            {
                Debug.Log("Enemy has died!");
                Destroy(gameObject);
            }
        }
        
    }
}