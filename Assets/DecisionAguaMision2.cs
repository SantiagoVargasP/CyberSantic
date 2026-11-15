
using UnityEngine;
using TMPro;

public class DecisionAguaMision2 : MonoBehaviour
{
    [Header("Recursos de agua")]
    public float agua = 100f;

    [Header("Conexiones")]
    public DecisionMision recursosBase;
    public RadiationSystem sistemaTraje;

    [Header("Consecuencias por segundo")]
    public float perdidaEnergia = 8f;
    public float perdidaComida = 6f;
    public float perdidaAgua = 5f;

    [Header("Interfaz")]
    public TextMeshProUGUI textoResultado;
    public GameObject[] botonesRespuestas;
    public GameObject[] textosOpciones;
    public GameObject botonContinuar;

    private bool fugaActiva = false;
    private bool cortocircuito = false;
    private bool decisionTomada = false;
    

    void Update()
    {
        if (!fugaActiva || recursosBase == null)
            return;

        agua = Mathf.Max(0f, agua - perdidaAgua * Time.deltaTime);
if (cortocircuito)
{
    recursosBase.energia = 0f;
    recursosBase.ActualizarBarras();
}
        else
        {
            // La fuga consume energía y alimentos.
            recursosBase.energia = Mathf.Max(
                0f, recursosBase.energia - perdidaEnergia * Time.deltaTime);

            recursosBase.comida = Mathf.Max(
                0f, recursosBase.comida - perdidaComida * Time.deltaTime);
                recursosBase.ActualizarBarras();
        }
    }
    
    public void ElegirRepararFuga()
    {
        if (decisionTomada) return;
        decisionTomada = true;

        fugaActiva = false;
        cortocircuito = false;

        if (sistemaTraje != null)
        {
            sistemaTraje.isSolarStormActive = false;
            sistemaTraje.isSafe = true;
        }

        MostrarResultado(
            "¡EXCELENTE! Reparaste la fuga. " +
            "El agua está protegida y la base puede seguir funcionando.",
            Color.green
        );
    }

    public void ElegirIgnorarFuga()
    {
        if (decisionTomada) return;
        decisionTomada = true;

        fugaActiva = true;
        cortocircuito = false;

        ActivarPeligroRadiacion();

        MostrarResultado(
            "¡ALERTA! Ignoraste la fuga. " +
            "La energía y la comida están disminuyendo. " +
            "El traje comienza a recibir radiación.",
            Color.yellow
        );
    }

    public void ElegirAumentarRiego()
    {
        if (decisionTomada) return;
        decisionTomada = true;

        fugaActiva = true;
        cortocircuito = true;

        // El exceso de agua provoca un cortocircuito.
        if (recursosBase != null)
            recursosBase.energia = 0f;

        ActivarPeligroRadiacion();

        MostrarResultado(
            "¡CORTOCIRCUITO! El exceso de agua apagó la energía. " +
            "La protección de la base falló y el traje recibe radiación.",
            Color.red
        );
    }

    void ActivarPeligroRadiacion()
    {
        if (sistemaTraje != null)
        {
            sistemaTraje.isSafe = false;
            sistemaTraje.isSolarStormActive = true;
        }
    }

    void MostrarResultado(string mensaje, Color color)
    {
        if (textoResultado != null)
        {
            textoResultado.text = mensaje;
            textoResultado.color = color;
        }

        MostrarContinuar();
    }

    void MostrarContinuar()
    {
        foreach (GameObject objeto in botonesRespuestas)
            if (objeto != null) objeto.SetActive(false);

        foreach (GameObject objeto in textosOpciones)
            if (objeto != null) objeto.SetActive(false);

        if (botonContinuar != null)
            botonContinuar.SetActive(true);
    }
}


