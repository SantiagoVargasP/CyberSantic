
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

    [Header("Pantalla Game Over")]
    public GameObject panelGameOver;

    [Header("Controles del Astronauta")]
    public Behaviour[] controlesJugador;

    private bool misionTerminada = false;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        if (panelGameOver != null)
        {
            panelGameOver.SetActive(false);
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (misionTerminada) return;

        // Recuperación de salud en zona segura
        if (isSafe)
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += (radiationDamagePerSecond / 2f) * Time.deltaTime;
            }

            ActualizarEstadoTraje(
                "ZONA SEGURA: Búnker subterráneo (Blindaje activo)",
                Color.cyan
            );
        }
        else if (isSolarStormActive)
        {
            currentHealth -= radiationDamagePerSecond * Time.deltaTime;

            ActualizarEstadoTraje(
                "¡PELIGRO! Mucha radiación solar",
                Color.red
            );
        }
        else
        {
            ActualizarEstadoTraje(
                "TRAJE NORMAL: Exploración en superficie",
                Color.green
            );
        }

        // Limitar salud entre 0 y 100
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        // Comprobar derrota
        if (currentHealth <= 0f)
        {
            ActivarGameOver();
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

    void ActivarGameOver()
    {
        if (misionTerminada) return;

        misionTerminada = true;
        currentHealth = 0f;

        ActualizarEstadoTraje(
            "TRAJE DESTRUIDO - MISIÓN FALLIDA",
            Color.red
        );

        if (healthBar != null)
        {
            healthBar.value = 0f;
        }

        // Mostrar pantalla de derrota
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
        }

        // Detener movimiento y cámara
        if (controlesJugador != null)
        {
            foreach (Behaviour control in controlesJugador)
            {
                if (control != null)
                {
                    control.enabled = false;
                }
            }
        }

        // Liberar el cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Debug.Log("GAME OVER: El astronauta ha perdido la misión.");
    }

    public void ReiniciarMision()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SafeZone"))
        {
            isSafe = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SafeZone"))
        {
            isSafe = false;
        }
    }
}
