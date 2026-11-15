using UnityEngine.UI;
using UnityEngine;

public class CultivoMision2 : MonoBehaviour
{
    public Animator animacionPlanta;
    public DecisionMision recursosBase;
    public Slider sliderEnergia;

    public void Sembrar()
    {
        if (recursosBase == null || recursosBase.energia < 40f)
        {
            Debug.Log("No hay energía suficiente para sembrar.");
            return;
        }

        recursosBase.energia -= 30f;
        animacionPlanta.Play("CrecerPlanta", 0, 0f);
    }

public void RecargarEnergia()
{
    if (recursosBase == null) return;

    recursosBase.energia =
    Mathf.Min(recursosBase.energia + 40f, 100f);

    Debug.Log("¡Energía recuperada! +30");
}

}

