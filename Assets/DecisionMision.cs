using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DecisionMision : MonoBehaviour
{
    [Header("Pantalla de Decisión")]
    public GameObject panelMenu; // El cuadro con los botones (Mision 1)
    public TextMeshProUGUI textoAviso; // El texto amarillo (debe estar AFUERA de Mision 1)
    public GameObject botonContinuar; // El botón Continuar (debe estar AFUERA de Mision 1)

    [Header("Recursos de la Base")]
    public float energia = 100f;
    public float comida = 100f;

    [Header("Conexión con el Astronauta")]
    public RadiationSystem sistemaTraje;

    [Header("Misiones")]
    public DecisionMision scriptMisionTormenta;

    public bool yaDecidio = false;

    [Header("Barras de Recursos")]
    public Slider sliderEnergia;
    public Slider sliderComida;

    void Start()
    {
        if (panelMenu != null) panelMenu.SetActive(false);
        if (botonContinuar != null) botonContinuar.SetActive(false);
        if (textoAviso != null) textoAviso.text = "";
        ActualizarBarras();
    }

    public void MostrarMenuTormenta()
    {
        if (yaDecidio) return;

        if (panelMenu != null) panelMenu.SetActive(true);
        if (botonContinuar != null) botonContinuar.SetActive(false);
        if (textoAviso != null) textoAviso.text = "";

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ElegirEscudoMaximo()
    {
        yaDecidio = true; // Evita que el menú vuelva a salir al caminar por el búnker
        energia -= 60f;
        comida -= 20f;

        energia -= 60f;
        comida -= 20f;

        ActualizarBarras();

        if (panelMenu != null) panelMenu.SetActive(false);

        if (textoAviso != null)
        {
            textoAviso.text = "Escudo al 100%. Gastaste mucha energía y las plantas no crecerán hoy.";
            textoAviso.color = Color.yellow;
        }

        if (botonContinuar != null)
        {
            botonContinuar.SetActive(true);
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (sistemaTraje != null) sistemaTraje.isSafe = true;
    }

    public void ElegirEscudoMedio()
    {
        yaDecidio = true; // Evita que el menú vuelva a salir al caminar por el búnker
        energia -= 10f;
        ActualizarBarras();

        if (panelMenu != null) panelMenu.SetActive(false);

        if (textoAviso != null)
        {
            textoAviso.text = "Escudo Medio. Ahorraste energía, pero recibiste radiación.";
            textoAviso.color = Color.yellow;
        }

        if (botonContinuar != null)
        {
            botonContinuar.SetActive(true);
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (sistemaTraje != null)
        {
            sistemaTraje.isSafe = false;
            sistemaTraje.currentHealth -= 30f;
        }
    }

    public void BotonContinuarAccion()
    {
        if (textoAviso != null) textoAviso.text = "";
        if (botonContinuar != null) botonContinuar.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
     public void ActualizarBarras()
{
    if (sliderEnergia != null)
        sliderEnergia.value = energia;

    if (sliderComida != null)
        sliderComida.value = comida;
}
}