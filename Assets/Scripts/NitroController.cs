using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NitroController : MonoBehaviour
{
    public float maxNitro = 100f; 
    public float nitroGainRate = 10f;  
    public float nitroDuration = 5f;  
    public float nitroMultiplier = 3f;  
    public Button nitroButton;  
    public Slider nitroSlider;

    private float currentNitro = 0f;
    private bool isNitroActive = false;
    private float normalSpeed;

    private CarController carController;

    void Start()
    {
        carController = GetComponent<CarController>();

        normalSpeed = carController.acceleration;

        nitroButton.interactable = false;

        nitroButton.onClick.AddListener(ActivateNitro);

        if (nitroSlider != null)
        {
            nitroSlider.maxValue = maxNitro;
            nitroSlider.value = currentNitro;
        }
    }

    void Update()
    {
        if (!isNitroActive)
        {
            if (currentNitro < maxNitro)
            {
                currentNitro += nitroGainRate * Time.deltaTime;

                if (nitroSlider != null)
                {
                    nitroSlider.value = currentNitro;
                }

                if (currentNitro >= maxNitro)
                {
                    nitroButton.interactable = true;
                }
            }
        }
    }

    void ActivateNitro()
    {
        if (currentNitro >= maxNitro)
        {
            StartCoroutine(NitroBoost());
        }
    }

    IEnumerator NitroBoost()
    {
        isNitroActive = true;
        nitroButton.interactable = false;  
        carController.acceleration *= nitroMultiplier;  

        yield return new WaitForSeconds(nitroDuration);  

        carController.acceleration = normalSpeed;  
        currentNitro = 0f;  
        isNitroActive = false;

        if (nitroSlider != null)
        {
            nitroSlider.value = currentNitro;
        }
    }
}
