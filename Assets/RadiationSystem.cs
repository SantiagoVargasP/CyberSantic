using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RadiationSystem : MonoBehaviour
{
    [Header("Interfaz de Usuario")]
    public Slider healthBar;
    public TextMeshProUGUI suitStatusText;

    [Header("Estado del Traje")]
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isSafe = false;
    public bool isSolarStormActive = false;

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
        // Si la salud llega a 0, permitir reinicio con la tecla R
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            ActualizarEstadoTraje("TRAJE CRÍTICO - MISIÓN FALLIDA (Presiona R para reintentar)", Color.red);

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            return;
        }

        // Lógica de radiación y estado visual
        if (isSafe)
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += (radiationDamagePerSecond / 2f) * Time.deltaTime;
            }
            ActualizarEstadoTraje("ZONA SEGURA: Búnker subterráneo (Blindaje activo)", Color.cyan);
        }
        else if (isSolarStormActive)
        {
            currentHealth -= radiationDamagePerSecond * Time.deltaTime;
            ActualizarEstadoTraje("¡PELIGRO! Radiación solar extrema en superficie", Color.red);
        }
        else
        {
            ActualizarEstadoTraje("TRAJE NOMINAL: Exploración en superficie", Color.green);
        }

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
    }

    void ActualizarEstadoTraje(string mensaje, Color color)
    {
        if (suitStatusText != null)
        {
            suitStatusText.text = mensaje;
            suitStatusText.color = color;
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