
using UnityEngine;
using UnityEngine.UI;

public class CultivoMision2 : MonoBehaviour
{
    [Header("Planta")]
    public Animator animacionPlanta;

    [Header("Recursos")]
    public DecisionMision recursosBase;
    public Slider sliderEnergia;
    [Header("Activar Misión 2")]
public GameObject panelMision2;

    void Start()
    {
        ActualizarBarraEnergia();
        if (panelMision2 != null)
    panelMision2.SetActive(false);
    }

    public void Sembrar()
    {
        if (recursosBase == null) return;

        if (recursosBase.energia < 30f)
        {
            Debug.Log("No hay energía suficiente para sembrar.");
            return;
        }

        recursosBase.energia -= 30f;

        ActualizarBarraEnergia();

        if (animacionPlanta != null)
        {
            animacionPlanta.Play("CrecerPlanta", 0, 0f);

            if (panelMision2 != null)
    panelMision2.SetActive(true);
        }

        Debug.Log("¡Cultivo sembrado! -30 energía.");
    }

    public void RecargarEnergia()
    {
        if (recursosBase == null) return;

        recursosBase.energia =
            Mathf.Min(recursosBase.energia + 40f, 100f);

        ActualizarBarraEnergia();

        Debug.Log("¡Energía recuperada! +40");
    }

    void ActualizarBarraEnergia()
    {
        if (sliderEnergia != null && recursosBase != null)
        {
            sliderEnergia.maxValue = 100f;
            sliderEnergia.value = recursosBase.energia;
        }
    }
}
