using UnityEngine;
using UnityEngine.UI;

public class RadiationSystem : MonoBehaviour
{
    [Header("Interfaz de Usuario")]
    public Slider healthBar;

    [Header("Estado del Traje")]
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isSafe = false;
    public bool isSolarStormActive = false; // Controlado por la API DONKI

    [Header("Peligros del Entorno")]
    public float radiationDamagePerSecond = 5f; 

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    void Update()
    {
        // El daño solo ocurre si NO estamos a salvo Y HAY tormenta solar activa
        if (!isSafe && isSolarStormActive && currentHealth > 0)
        {
            currentHealth -= radiationDamagePerSecond * Time.deltaTime;
        }
        else if (isSafe && currentHealth < maxHealth)
        {
            currentHealth += (radiationDamagePerSecond / 2f) * Time.deltaTime;
        }

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SafeZone")) isSafe = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SafeZone")) isSafe = false;
    }
}